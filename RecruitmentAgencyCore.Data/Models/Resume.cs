using System;
using System.Collections.Generic;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public virtual JobSeeker JobSeeker { get; set; }
        public string Position { get; set; }
        public int? Salary { get; set; }
        public string Skills { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int? CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public int? BranchId { get; set; } // отрасль > специальность
        public virtual Branch Branch { get; set; }
        public int? SpecialityId { get; set; } // специальность
        public virtual Branch Speciality { get; set; }

        public int? TypeOfEmploymentId { get; set; } // тип занятости
        public virtual TypeOfEmployment TypeOfEmployment { get; set; }

        public int? ScheduleId { get; set; } // график работы
        public virtual Schedule Schedule { get; set; }

    }
}