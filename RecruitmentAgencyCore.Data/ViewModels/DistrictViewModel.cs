using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.ViewModels
{
    public class DistrictViewModel
    {
        public DistrictViewModel()
        {

        }
        public DistrictViewModel(District district)
        {
            Id = district.Id;
            NameUz = district.NameUz;
            NameRu = district.NameRu;
            NameEn = district.NameEn;
            RegionId = district.RegionId;
        }

        public int Id { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public int? RegionId { get; set; }
    }
}
