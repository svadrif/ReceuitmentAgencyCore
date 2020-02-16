using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class PreviousWork
    {
        public int Id { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrent { get; set; }
        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }

        public int? JobSeekerId { get; set; }
        public virtual JobSeeker JobSeeker { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
