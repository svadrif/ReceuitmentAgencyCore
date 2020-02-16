using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceuitmentAgencyCore.Models
{
    public class MenuViewModel
    {
        public MenuViewModel(Menu menu)
        {
            Id = menu.Id;
            CaptionUz = menu.CaptionUz;
            CaptionRu = menu.CaptionRu;
            CaptionEn = menu.CaptionEn;
            Icon = menu.Icon;
            Style = menu.Style;
            Url = menu.Url;
            TypeId = menu.TypeId;
            ParentId = menu.ParentId;
            CreatedBy = menu.CreatedBy;
            CreatedDate = menu.CreatedDate;
            ModifiedBy = menu.ModifiedBy;
            ModifiedDate = menu.ModifiedDate;
        }
        public int Id { get; set; }
        public string CaptionUz { get; set; }
        public string CaptionRu { get; set; }
        public string CaptionEn { get; set; }
        public string Icon { get; set; }
        public string Style { get; set; }
        public string Url { get; set; }
        public int? TypeId { get; set; }
        public int? ParentId { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
