using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Response
    {
        public int Id { get; set; }
        public int? VacancyId { get; set; }
        public virtual Vacancy Vacancy { get; set; }

        public int? JobSeekerId { get; set; }
        public virtual JobSeeker JobSeeker { get; set; }

    }
}