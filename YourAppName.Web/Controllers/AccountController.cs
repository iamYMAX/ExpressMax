using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using YourAppName.Data.Models;
using YourAppName.Web.ViewModels.AccountViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Required for [Authorize] if used on controller

namespace YourAppName.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken] // Good practice for logout as well
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // Optionally, log user logout
            // _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home"); // Or redirect to login page or a specific logged-out page
        }
    }
}
