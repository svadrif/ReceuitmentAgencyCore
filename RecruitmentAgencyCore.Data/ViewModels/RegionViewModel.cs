using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.ViewModels
{
    public class RegionViewModel
    {
        public RegionViewModel()
        {

        }
        public RegionViewModel(Region region)
        {
            Id = region.Id;
            NameUz = region.NameUz;
            NameRu = region.NameRu;
            NameEn = region.NameEn;
            CountryId = region.CountryId;
        }

        public int Id { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public int? CountryId { get; set; }
    }
}
