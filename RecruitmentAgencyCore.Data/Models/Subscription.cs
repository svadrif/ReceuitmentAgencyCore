using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int? JobSeekerId { get; set; }
        public virtual JobSeeker JobSeeker { get; set; }
        public int? EmployerId { get; set; }
        public virtual Employer Employer { get; set; }

    }
}
