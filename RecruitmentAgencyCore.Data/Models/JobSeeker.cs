using System;
using System.Collections.Generic;

namespace RecruitmentAgencyCore.Data.Models
{
    public class JobSeeker
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhotoPath { get; set; }
        public string AboutMe { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string LinkedIn { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public bool? IsReadyForMission { get; set; }
        public bool? IsReadyForRelocation { get; set; }
        public string DriverLicense { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public int? CitizenshipId { get; set; }
        public virtual Citizenship Citizenship { get; set; }
        public int? SocialStatusId { get; set; }
        public virtual SocialStatus SocialStatus { get; set; }
        public int? FamilyStatusId { get; set; }
        public virtual FamilyStatus FamilyStatus { get; set; }
        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }
        public int? DistrictId { get; set; }
        public virtual District District { get; set; }
        public string Address { get; set; } 
        public virtual ICollection<PreviousWork> PreviousWorkList { get; set; }
        public virtual ICollection<JobSeekerForeignLanguage> JobSeekerForeignLanguages { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
        public virtual ICollection<Subscription> Subscribes { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
