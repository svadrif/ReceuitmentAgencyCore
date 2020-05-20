using RecruitmentAgencyCore.Data.Models;
using System.Collections.Generic;

namespace RecruitmentAgencyCore.Data.ViewModels
{
    public class JobSeekerViewModel
    {
        public JobSeekerViewModel()
        {

        }

        public JobSeekerViewModel(JobSeeker jobSeeker)
        {
            Id = jobSeeker.Id;
            FullName = jobSeeker.FullName;
            PhotoPath = jobSeeker.PhotoPath;
            AboutMe = jobSeeker.AboutMe;
            BirthDate = jobSeeker.BirthDate?.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Website = jobSeeker.Website;
            Phone = jobSeeker.PhotoPath;
            Mail = jobSeeker.Mail;
            LinkedIn = jobSeeker.LinkedIn;
            Facebook = jobSeeker.Facebook;
            Instagram = jobSeeker.Instagram;
            IsReadyForMission = jobSeeker.IsReadyForMission.Value;
            IsReadyForRelocation = jobSeeker.IsReadyForRelocation.Value;
            DriverLicense = jobSeeker.DriverLicense.ToList();
            UserId = jobSeeker.UserId;
            CitizenshipId = jobSeeker.CitizenshipId;
            SocialStatusId = jobSeeker.SocialStatusId;
            FamilyStatusId = jobSeeker.FamilyStatusId;
            GenderId = jobSeeker.GenderId;
            CountryId = jobSeeker.CountryId;
            RegionId = jobSeeker.RegionId;
            DistrictId = jobSeeker.DistrictId;
            Address = jobSeeker.Address;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhotoPath { get; set; }
        public string AboutMe { get; set; }
        public string BirthDate { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string LinkedIn { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public bool IsReadyForMission { get; set; }
        public bool IsReadyForRelocation { get; set; }
        public IList<string> DriverLicense { get; set; }

        public int? UserId { get; set; }
        public int? CitizenshipId { get; set; }
        public int? SocialStatusId { get; set; }
        public int? FamilyStatusId { get; set; }
        public int? GenderId { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? DistrictId { get; set; }
        public string Address { get; set; }

    }
}
