﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Citizenship
    {
        public int Id { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }

        public virtual ICollection<JobSeeker> JobSeekers { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
