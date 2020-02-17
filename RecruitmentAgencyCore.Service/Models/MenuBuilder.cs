using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Data.ViewModels;
using RecruitmentAgencyCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Service.Models
{
    public class MenuBuilder : IMenuBuilder
    {
        private readonly IGenericRepository<Menu> _menuRepo;
        public MenuBuilder(IGenericRepository<Menu> menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public async Task<List<MenuViewModel>> GetChildrenAsync(MenuViewModel menu)
        {
            ICollection<Menu> ch = await _menuRepo.FindAllAsync(x => x.ParentId == menu.Id);
            List<MenuViewModel> children = ch.Select(x => new MenuViewModel(x)).ToList();
            return children;
        }

        public List<MenuViewModel> GetMenu(ICollection<MenuRolePermission> menuRolePermissions)
        {
            List<MenuViewModel> res = menuRolePermissions.Where(x => x.Menu?.ParentId == 0)?.Select(x => new MenuViewModel(x.Menu)).ToList();
            res.ForEach(async x => x.ChildrenMenus = await GetChildrenAsync(x));
            return res;
        }
    }
}