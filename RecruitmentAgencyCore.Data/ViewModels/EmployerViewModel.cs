using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.ViewModels
{
    public class EmployerViewModel
    {
        public EmployerViewModel()
        {

        }
        public EmployerViewModel(Employer employer)
        {
            Id = employer.Id;
            Name = employer.Name;
            Logo = employer.Logo;
            Description = employer.Description;
            StaffCount = employer.StaffCount;
            Website = employer.Website;
            Phone = employer.Phone;
            Mail = employer.Mail;
            LinkedIn = employer.LinkedIn;
            Facebook = employer.Facebook;
            Instagram = employer.Instagram;
            Address = employer.Address;
            CountryId = employer.CountryId;
            RegionId = employer.RegionId;
            DistrictId = employer.DistrictId;
            UserId = employer.UserId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string StaffCount { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string LinkedIn { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }

        public int? UserId { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? DistrictId { get; set; }
        public string Address { get; set; }

    }
}
