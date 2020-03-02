using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.ViewModels
{
    public class CountryViewModel
    {
        public CountryViewModel()
        {

        }
        public CountryViewModel(Country country)
        {
            Id = country.Id;
            NameUz = country.NameUz;
            NameRu = country.NameRu;
            NameEn = country.NameEn;
        }
        public int Id { get; set; }
        public string NameUz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
    }
}
