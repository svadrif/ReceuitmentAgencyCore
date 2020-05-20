using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentAgencyCore.Data.ViewModels
{
    public static class Extensions
    {      
        public static Employer AsEmployer(this EmployerViewModel employer)
        {
            return new Employer
            {
                Id = employer.Id,
                Name = employer.Name,
                Logo = employer.Logo,
                Description = employer.Description,
                StaffCount = employer.StaffCount,
                Website = employer.Website,
                Phone = employer.Phone,
                Mail = employer.Mail,
                LinkedIn = employer.LinkedIn,
                Facebook = employer.Facebook,
                Instagram = employer.Instagram,
                Address = employer.Address,
                CountryId = employer.CountryId,
                RegionId = employer.RegionId,
                DistrictId = employer.DistrictId,
                UserId = employer.UserId
            };
        }

        public static JobSeeker AsJobSeeker(this JobSeekerViewModel model)
        {
            return new JobSeeker
            {
                Id = model.Id,
                FullName = model.FullName,
                PhotoPath = model.PhotoPath,
                AboutMe = model.AboutMe,
                BirthDate = DateTime.ParseExact(model.BirthDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                Website = model.Website,
                Phone = model.Phone,
                Mail = model.Mail,
                LinkedIn = model.LinkedIn,
                Facebook = model.Facebook,
                Instagram = model.Instagram,
                IsReadyForMission = model.IsReadyForMission,
                IsReadyForRelocation = model.IsReadyForRelocation,
                DriverLicense = ToString(model.DriverLicense),
                UserId = model.UserId,
                CitizenshipId = model.CitizenshipId,
                SocialStatusId = model.SocialStatusId,
                FamilyStatusId = model.FamilyStatusId,
                GenderId = model.GenderId,
                CountryId = model.CountryId,
                RegionId = model.RegionId,
                DistrictId = model.DistrictId,
                Address = model.Address
            };
        }

        public static string[] ToArray(this string str)
        {
            return str.Split(',');
        }

        public static string ToString(this string[] arr)
        {
            return string.Join(',', arr);
        }

        public static IList<string> ToList(this string str)
        {
            return str.Split(',').ToList();
        }

        public static string ToString(this IList<string> list)
        {
            return string.Join(',', list);
        }
    }
}
