using Microsoft.Extensions.Logging;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Data.ViewModels;
using RecruitmentAgencyCore.Service.Interfaces;
using RecruitmentAgencyCore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Helpers
{
    public class RouteHelper
    {
        private readonly ILogger<RouteHelper> _logger;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<JobSeeker> _jobSeekerRepository;
        private readonly IGenericRepository<Employer> _employerRepository;
        private readonly IGenericRepository<MenuRolePermission> _menuRolePermissionRepository;
        private readonly IMenuBuilder _menuBuilder;
        public RouteHelper(ILogger<RouteHelper> logger, IGenericRepository<User> userRepository,
            IGenericRepository<MenuRolePermission> menuRolePermissionRepository, IMenuBuilder menuBuilder,
            IGenericRepository<JobSeeker> jobSeekerRepository, IGenericRepository<Employer> employerRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _menuRolePermissionRepository = menuRolePermissionRepository;
            _menuBuilder = menuBuilder;
            _jobSeekerRepository = jobSeekerRepository;
            _employerRepository = employerRepository;
        }
        public async Task<MenuViewModel> GetMenuByEmailAsync(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    User user = await _userRepository.FindAsync(x => x.Email == email);
                    GetCurrentUser(user);
                    ICollection<MenuRolePermission> menuRolePermissions = _menuRolePermissionRepository
                                .GetAllIncluding(r => r.Role, p => p.Permission, m => m.Menu)
                                .ToList().FindAll(x => x.RoleId == user.RoleId);
                    MenuModel.GetMenuViewModels = _menuBuilder.GetMenu(menuRolePermissions);
                    MenuViewModel menu = MenuModel.GetMenuViewModels?.FirstOrDefault();
                    return menu;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;

        }

        public void GetCurrentUser(User user)
        {
            if (user.RoleId == 3)
            {
                JobSeeker jobseeker = _jobSeekerRepository.GetAllIncluding(u => u.User, c => c.Country, r => r.Region, d => d.District).ToList().Find(x => x.UserId == user.UserId);
                if (jobseeker != null)
                {
                    UserModel.JobSeeker = new JobSeekerViewModel(jobseeker);
                }
            }
            else if (user.RoleId == 2)
            {
                Employer employer = _employerRepository.GetAllIncluding(u => u.User, c => c.Country, r => r.Region, d => d.District).ToList().Find(x => x.UserId == user.UserId);
                if (employer != null)
                {
                    UserModel.Employer = new EmployerViewModel(employer);
                }
            }
        }

        public void DestroyCurrentUser()
        {
            (UserModel.JobSeeker, UserModel.Employer, MenuModel.GetMenuViewModels) = (null, null, null);
        }
    }
}
