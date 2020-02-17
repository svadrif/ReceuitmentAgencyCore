using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Service.Interfaces
{
    public interface IMenuBuilder
    {
        public IEnumerable<MenuViewModel> GetMenu(ICollection<MenuRolePermission> menuRolePermissions);
        public IEnumerable<MenuViewModel> GetChildren(MenuViewModel menu);
    }
}
