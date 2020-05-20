using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.ViewModels
{
    public class RegionViewModel : CountryViewModel
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

        public int? CountryId { get; set; }
    }
}
