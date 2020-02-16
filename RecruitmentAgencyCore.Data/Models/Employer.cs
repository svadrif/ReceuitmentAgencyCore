using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Employer
    {
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
        public virtual User User { get; set; }


        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }
        public int? DistrictId { get; set; }
        public virtual District District { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Vacancy> Vacancies { get; set; }
        public virtual ICollection<Subscription> Subscribers { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
