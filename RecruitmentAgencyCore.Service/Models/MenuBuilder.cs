using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Data.ViewModels;
using RecruitmentAgencyCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecruitmentAgencyCore.Service.Models
{
    public class MenuBuilder : IMenuBuilder
    {
        private readonly IGenericRepository<Menu> _menuRepo;
        public MenuBuilder(IGenericRepository<Menu> menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public IEnumerable<MenuViewModel> GetChildren(MenuViewModel menu)
        {
            List<MenuViewModel> res = new List<MenuViewModel>();
            IEnumerable<MenuViewModel> children = _menuRepo.FindAll(x => x.ParentId == menu.ParentId).Select(x => new MenuViewModel(x));
            return children;
        }

        public IEnumerable<MenuViewModel> GetMenu(ICollection<MenuRolePermission> menuRolePermissions)
        {
            IEnumerable<MenuViewModel> res = menuRolePermissions.Where(x => x.Menu.ParentId == 0).Select(x => new MenuViewModel(x.Menu));
            res.AsParallel().ForAll(x => x.MenuViewModels = GetChildren(x));
            return res;
        }
    }
}