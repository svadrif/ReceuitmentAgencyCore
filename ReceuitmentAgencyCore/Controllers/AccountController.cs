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

namespace RecruitmentAgencyCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly RecruitmentAgencySignInManager _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<MenuRolePermission> _menuRolePermissionRepository;
        private readonly IMenuBuilder _menuBuilder;

        public AccountController(ILogger<AccountController> logger,
            RecruitmentAgencySignInManager signInManager, IGenericRepository<User> userRepository,
            IGenericRepository<MenuRolePermission> menuRolePermissionRepository, IMenuBuilder menuBuilder)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _menuRolePermissionRepository = menuRolePermissionRepository;
            _menuBuilder = menuBuilder;
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
            User user = await _userRepository.FindAsync(x => x.Email == email);
            ICollection<MenuRolePermission> menuRolePermissions = _menuRolePermissionRepository.GetAllIncluding(r => r.Role, p => p.Permission, m => m.Menu).ToList().FindAll(x => x.RoleId == user.RoleId);
            MenuService.GetMenuViewModels = _menuBuilder.GetMenu(menuRolePermissions);
            MenuViewModel menu = _menuBuilder.GetMenu(menuRolePermissions)?.FirstOrDefault();
            return RedirectToAction(menu?.Action, menu?.Controller);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            MenuService.GetMenuViewModels = null;
            return RedirectToAction("Index", "Home");
        }
    }
}