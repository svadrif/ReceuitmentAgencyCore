using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReceuitmentAgencyCore.Security;
using ReceuitmentAgencyCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Data.Models;

namespace ReceuitmentAgencyCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly RecruitmentAgencySignInManager _signInManager;
        private readonly IGenericRepository<User> _userRepository;
        public AccountController(ILogger<AccountController> logger,
            RecruitmentAgencySignInManager signInManager, IGenericRepository<User> userRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "JobSeeker");
            }

            return View();


        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("Email", model.Email);
                    return RedirectToAction("Index", "JobSeeker");
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out");
                    return View("Lock");
                }
            }
            _logger.Log(LogLevel.Error, "User not found");
            ModelState.AddModelError(string.Empty, "Failed to login");
            return View(model);
        }

        public async Task DefineRoleAndRedirectToAction(string email)
        {
            var user = await _userRepository.FindAsync(x => x.Email == email);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}