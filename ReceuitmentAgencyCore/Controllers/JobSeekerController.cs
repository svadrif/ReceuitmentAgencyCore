using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using RecruitmentAgencyCore.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecruitmentAgencyCore.Data.ViewModels;
using RecruitmentAgencyCore.Service.Models;

namespace RecruitmentAgencyCore.Controllers
{
    public class JobSeekerController : BaseController
    {
        private readonly ILogger<JobSeekerController> _logger;

        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<JobSeeker> _jobSeekerRepo;
        private readonly IGenericRepository<Country> _countryRepo;
        private readonly IGenericRepository<Region> _regionRepo;
        private readonly IGenericRepository<District> _districtRepo;
        private readonly IGenericRepository<Gender> _genderRepo;
        private readonly IGenericRepository<Citizenship> _citizenshipRepo;
        private readonly IGenericRepository<SocialStatus> _socialStatusRepo;
        private readonly IGenericRepository<FamilyStatus> _familyStatusRepo;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly RouteHelper _routeHelper;
        private readonly FileHelper _fileHelper;
        public JobSeekerController(ILogger<JobSeekerController> logger, IGenericRepository<User> userRepo,
            IGenericRepository<JobSeeker> jobSeekerRepo, IGenericRepository<Country> countryRepo,
            IGenericRepository<Region> regionRepo, IGenericRepository<District> districtRepo, IGenericRepository<Gender> genderRepo,
            IGenericRepository<Citizenship> citizenshipRepo, IGenericRepository<SocialStatus> socialStatusRepo,
            IGenericRepository<FamilyStatus> familyStatusRepo,
            IWebHostEnvironment webHostEnvironment, RouteHelper routeHelper, FileHelper fileHelper, IGenericRepository<Logging> loggingRepo) : base(userRepo, loggingRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
            _jobSeekerRepo = jobSeekerRepo;
            _countryRepo = countryRepo;
            _regionRepo = regionRepo;
            _districtRepo = districtRepo;
            _webHostEnvironment = webHostEnvironment;
            _routeHelper = routeHelper;
            _fileHelper = fileHelper;
            _genderRepo = genderRepo;
            _citizenshipRepo = citizenshipRepo;
            _socialStatusRepo = socialStatusRepo;
            _familyStatusRepo = familyStatusRepo;
        }

        public IActionResult Details()
        {  
            return View();
        }

        public async Task<IActionResult> Register(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                User user = await _userRepo.FindAsync(x => x.Email == email);
                if (user != null)
                {
                    if (_jobSeekerRepo.FindAll(x => x.UserId == user.UserId).Count() == 0)
                    {
                        HttpContext?.Session?.SetInt32("RegisteredUserId", user.UserId);
                        HttpContext?.Session?.SetString("RegUserEmail", email);

                        ViewBag.DriverLicenses = new SelectList(Utility.GetDriverLicenses(), "Text", "Value");
                        ViewBag.FamilyStatus = new SelectList(_familyStatusRepo.GetAll().ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                        ViewBag.SocialStatus = new SelectList(_socialStatusRepo.GetAll().ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                        ViewBag.Citizenship = new SelectList(_citizenshipRepo.GetAll().ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                        ViewBag.Gender = new SelectList(_genderRepo.GetAll().ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                        ViewBag.Country = new SelectList(_countryRepo.GetAll().Select(x => new CountryViewModel(x)).ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                        ViewBag.Region = new SelectList(_regionRepo.GetAll().Select(x => new RegionViewModel(x)).ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                        ViewBag.District = new SelectList(_districtRepo.GetAll().Select(x => new DistrictViewModel(x)).ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                        return View();
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormFile file, JobSeekerViewModel model)
        {
            int? regId = HttpContext?.Session?.GetInt32("RegisteredUserId");
            string email = HttpContext?.Session.GetString("RegUserEmail");
            if (regId != null)
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.PhotoPath = await _fileHelper.SaveFileAsync(file);
                    }
                    try
                    {
                        model.UserId = regId;


                        JobSeeker t = model.AsJobSeeker();
                        t.DriverLicense = Extensions.ToString(model.DriverLicense);

                        t.CreatedBy = regId;
                        t.CreatedDate = DateTime.Now.Date;

                        JobSeeker jobSeeker = _jobSeekerRepo.Add(t);

                        MenuViewModel menu = await _routeHelper.GetMenuByEmailAsync(email);
                        return RedirectToAction(menu.Action, menu.Controller);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);

                        return View();
                    }
                }

                ViewBag.DriverLicenses = new SelectList(Utility.GetDriverLicenses(), "Text", "Value");
                ViewBag.FamilyStatus = new SelectList(_familyStatusRepo.GetAll().ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                ViewBag.SocialStatus = new SelectList(_socialStatusRepo.GetAll().ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                ViewBag.Citizenship = new SelectList(_citizenshipRepo.GetAll().ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                ViewBag.Gender = new SelectList(_genderRepo.GetAll().ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                ViewBag.Country = new SelectList(_countryRepo.GetAll().Select(x => new CountryViewModel(x)).ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                ViewBag.Region = new SelectList(_regionRepo.GetAll().Select(x => new RegionViewModel(x)).ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");
                ViewBag.District = new SelectList(_districtRepo.GetAll().Select(x => new DistrictViewModel(x)).ToList(), "Id", ChangeNameByLangModel.Name ?? "NameUz");

                return View();
            }
            return RedirectToAction("Register", "Account");
        }
    }
}