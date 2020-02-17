using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitmentAgencyCore.Data.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string CaptionUz { get; set; }
        public string CaptionRu { get; set; }
        public string CaptionEn { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Parameter { get; set; }
        public string Icon { get; set; }
        public string Style { get; set; }
        public int? TypeId { get; set; }
        public int? ParentId { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
