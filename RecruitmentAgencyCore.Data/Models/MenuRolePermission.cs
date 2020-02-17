using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class MenuRolePermission
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; } 
        public int? MenuId { get; set; }
        public Menu Menu { get; set; }
        public int? PermissionId { get; set; }
        public Permission Permission { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
