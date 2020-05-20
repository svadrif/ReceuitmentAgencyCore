using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitmentAgencyCore.Security;
using RecruitmentAgencyCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Service.Interfaces;
using RecruitmentAgencyCore.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using RecruitmentAgencyCore.Helpers;
using RecruitmentAgencyCore.Service.Services;

namespace RecruitmentAgencyCore.Controllers
{
    public class AccountController : BaseController
    {
        private readonly RecruitmentAgencySignInManager _signInManager;
        private readonly RecruitmentAgencyUserManager _userManager;
        private readonly RouteHelper _routeHelper;

        private readonly ILogger<AccountController> _logger;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<MenuRolePermission> _menuRolePermissionRepository;
        private readonly IMenuBuilder _menuBuilder;

        public AccountController(ILogger<AccountController> logger, RecruitmentAgencyUserManager userManager,
            RecruitmentAgencySignInManager signInManager, IGenericRepository<User> userRepository,
            IGenericRepository<MenuRolePermission> menuRolePermissionRepository, IMenuBuilder menuBuilder,
            RouteHelper routeHelper, IGenericRepository<Logging> loggingRepo) : base(userRepository, loggingRepo)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _menuRolePermissionRepository = menuRolePermissionRepository;
            _menuBuilder = menuBuilder;
            _userManager = userManager;
            _routeHelper = routeHelper;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (register.Password != register.PasswordConfirm)
                {
                    throw new InvalidOperationException("Failed to create new user");
                }
                var user = await _userManager.FindByEmailAsync(register.Email);
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = register.Email,
                        Email = register.Email,
                        PhoneNumber = register.PhoneNumber,
                        OpenPassword = register.Password,
                        CreatedDate = DateTime.Now,
                        IsOnline = true,
                        NormalizedUserName = register.Email.ToLower(),
                        NormalizedEmail = register.Email.ToLower(),
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        RoleId = register.RoleId,
                        IsActive = true
                    };
                    var res = await _userManager.CreateAsync(user, register.Password);
                    if (res != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Failed to create new user");
                    }
                    if (register.RoleId == 2) return RedirectToAction("Register", "Employer", new { email = register.Email });
                    return RedirectToAction("Register", "JobSeeker", new { email = register.Email });
                }
                ModelState.AddModelError(string.Empty, "Failed to create new user");
            }
            ModelState.AddModelError(string.Empty, "This email already exists");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {              
                return await DefineRoleAndRedirectToAction(User.Identity.Name);
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
                    return await DefineRoleAndRedirectToAction(model.Email);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out");
                    return View("Lock");
                }
            }
            _logger.Log(LogLevel.Error, "User not found");
            ModelState.AddModelError(string.Empty, "Failed to login");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                string callbackUrl = Url.Action("Reset", "Account", (userId: user.Id, code), protocol: HttpContext.Request.Scheme);

                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Reset password", $"Для сброса пароля пройдите по ссылке" +
                      $": <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        public async Task<RedirectToActionResult> DefineRoleAndRedirectToAction(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    MenuViewModel menu = await _routeHelper.GetMenuByEmailAsync(email);
                    return RedirectToAction(menu?.Action, menu?.Controller);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                await _signInManager.SignOutAsync();            
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _routeHelper.DestroyCurrentUser();            
            return RedirectToAction("Index", "Home");
        }
    }
}