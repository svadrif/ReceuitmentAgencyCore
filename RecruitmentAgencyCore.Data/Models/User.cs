using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RecruitmentAgencyCore.Data.Models
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
        public virtual ICollection<Employer> Employers { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool? IsOnline { get; set; }
    }
}