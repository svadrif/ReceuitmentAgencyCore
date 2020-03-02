using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Data.ViewModels;

namespace RecruitmentAgencyCore.Controllers
{
    public class GetListController : BaseController
    {
        private readonly IGenericRepository<Employer> _employerRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Country> _countryRepo;
        private readonly IGenericRepository<Region> _regionRepo;
        private readonly IGenericRepository<District> _districtRepo;
        public GetListController(IGenericRepository<Employer> employerRepo, IGenericRepository<User> userRepo,
            IGenericRepository<Country> countryRepo, IGenericRepository<Region> regionRepo,
            IGenericRepository<District> districtRepo, IGenericRepository<Logging> loggingRepo) : base(userRepo, loggingRepo)
        {
            _employerRepo = employerRepo;
            _userRepo = userRepo;
            _countryRepo = countryRepo;
            _regionRepo = regionRepo;
            _districtRepo = districtRepo;
        }

        public JsonResult GetRegionByCountryId(int countryId)
        {
            List<RegionViewModel> res = _regionRepo.FindAll(x => x.CountryId == countryId).Select(x => new RegionViewModel(x)).ToList();
            return Json(res);
        }

        public JsonResult GetDistrictByRegionId(int regionId)
        {
            List<DistrictViewModel> res = _districtRepo.FindAll(x => x.RegionId == regionId).Select(x => new DistrictViewModel(x)).ToList();
            return Json(res);
        }
    }
}