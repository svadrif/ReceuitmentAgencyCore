using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Education
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public virtual JobSeeker JobSeeker { get; set; }
        public int? EnteredYear { get; set; }
        public int? GraduatedYear { get; set; }
        public string UniversityName { get; set; }
        public string Speciality { get; set; }
        public int? UniversityId { get; set; }
        public virtual University University { get; set; }
        public int? EducationTypeId { get; set; }
        public virtual EducationType EducationType { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
