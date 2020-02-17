using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Service.Interfaces
{
    public interface IMenuBuilder
    {
        public List<MenuViewModel> GetMenu(ICollection<MenuRolePermission> menuRolePermissions);
        public Task<List<MenuViewModel>> GetChildrenAsync(MenuViewModel menu);
    }
}
