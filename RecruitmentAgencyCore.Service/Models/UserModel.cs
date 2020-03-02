using RecruitmentAgencyCore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Service.Models
{
    public class UserModel
    {
        public static JobSeekerViewModel JobSeeker { get; set; }
        public static EmployerViewModel Employer { get; set; }
    }
}
