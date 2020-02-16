using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class JobSeekerForeignLanguage
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public virtual JobSeeker JobSeeker { get; set; }
        public int? ForeignLanguageId { get; set; }
        public virtual ForeignLanguage ForeignLanguage { get; set; }
        public int? LanguageLevelId { get; set; }
        public virtual LanguageLevel LanguageLevel { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
