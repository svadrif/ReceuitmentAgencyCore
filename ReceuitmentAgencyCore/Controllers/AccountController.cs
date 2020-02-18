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
using RecruitmentAgencyCore.Service.Services;
using Microsoft.AspNetCore.Identity;

namespace RecruitmentAgencyCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly RecruitmentAgencySignInManager _signInManager;
        private readonly RecruitmentAgencyUserManager _userManager;

        private readonly ILogger<AccountController> _logger;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<MenuRolePermission> _menuRolePermissionRepository;
        private readonly IMenuBuilder _menuBuilder;

        public AccountController(ILogger<AccountController> logger, RecruitmentAgencyUserManager userManager,
            RecruitmentAgencySignInManager signInManager, IGenericRepository<User> userRepository,
            IGenericRepository<MenuRolePermission> menuRolePermissionRepository, IMenuBuilder menuBuilder)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _menuRolePermissionRepository = menuRolePermissionRepository;
            _menuBuilder = menuBuilder;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
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
                        RoleId = register.RoleId,
                        IsActive = true
                    };
                    var res = await _userManager.CreateAsync(user, register.Password);
                    if (res != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Failed to create new user");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Failed to create new user");
            return View();
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
            return View(model);
        }

        public async Task<RedirectToActionResult> DefineRoleAndRedirectToAction(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                User user = await _userRepository.FindAsync(x => x.Email == email);
                ICollection<MenuRolePermission> menuRolePermissions = _menuRolePermissionRepository.GetAllIncluding(r => r.Role, p => p.Permission, m => m.Menu).ToList().FindAll(x => x.RoleId == user.RoleId);
                MenuService.GetMenuViewModels = _menuBuilder.GetMenu(menuRolePermissions);
                MenuViewModel menu = MenuService.GetMenuViewModels?.FirstOrDefault();
                return RedirectToAction(menu?.Action, menu?.Controller);
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
          
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            MenuService.GetMenuViewModels = null;
            return RedirectToAction("Index", "Home");
        }
    }
}