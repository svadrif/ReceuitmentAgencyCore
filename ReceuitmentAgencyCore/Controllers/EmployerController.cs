using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Data.ViewModels;
using RecruitmentAgencyCore.Helpers;

namespace RecruitmentAgencyCore.Controllers
{
    public class EmployerController : BaseController
    {
        private readonly IGenericRepository<Employer> _employerRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Country> _countryRepo;
        private readonly IGenericRepository<Region> _regionRepo;
        private readonly IGenericRepository<District> _districtRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployerController> _logger;

        private readonly RouteHelper _routeHelper;
        private readonly FileHelper _fileHelper;

        public EmployerController(IGenericRepository<Employer> employerRepo, IGenericRepository<User> userRepo, 
            IGenericRepository<Country> countryRepo, IGenericRepository<Region> regionRepo,
            IGenericRepository<District> districtRepo, IWebHostEnvironment webHostEnvironment, 
            ILogger<EmployerController> logger, FileHelper fileHelper, RouteHelper routeHelper, IGenericRepository<Logging> loggingRepo) : base(userRepo, loggingRepo)
        { 
            _employerRepo = employerRepo;
            _userRepo = userRepo;
            _countryRepo = countryRepo;
            _regionRepo = regionRepo;
            _districtRepo = districtRepo;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _fileHelper = fileHelper;
            _routeHelper = routeHelper;
        }
        public IActionResult Details()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                User user = await _userRepo.FindAsync(x => x.Email == email);
                if (user != null)
                {
                    if (_employerRepo.FindAll(x => x.UserId == user.UserId).Count() == 0)
                    {
                        HttpContext?.Session?.SetInt32("RegisteredUserId", user.UserId);
                        HttpContext?.Session?.SetString("RegUserEmail", email);
                        ViewBag.Country = new SelectList(_countryRepo.GetAll().Select(x => new CountryViewModel(x)).ToList(), "Id", "NameEn");
                        ViewBag.Region = new SelectList(_regionRepo.GetAll().Select(x => new RegionViewModel(x)).ToList(), "Id", "NameEn");
                        ViewBag.District = new SelectList(_districtRepo.GetAll().Select(x => new DistrictViewModel(x)).ToList(), "Id", "NameEn");
                        return View();
                    }
                    return RedirectToAction("Index", "Home");
                }              
            }
            return RedirectToAction("Index", "Home");           
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormFile file, EmployerViewModel model)
        {
            int? regId = HttpContext?.Session?.GetInt32("RegisteredUserId");
            string email = HttpContext?.Session.GetString("RegUserEmail");
            if (regId != null)
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.Logo = await _fileHelper.SaveFileAsync(file);
                    }
                    try
                    {
                        model.UserId = regId;

                        Employer emp = model.AsEmployer();
                        emp.CreatedDate = DateTime.Now;
                        emp.CreatedBy = regId;

                        Employer employer = _employerRepo.Add(emp);

                        MenuViewModel menu = await _routeHelper.GetMenuByEmail(email);

                        if (menu.TypeId == 1)
                            return RedirectToAction(menu.Action, menu.Controller);
                        else
                            return Redirect(menu.Url);
                        
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);

                        return View();
                    }                  
                }
                ViewBag.Country = new SelectList(_countryRepo.GetAll().Select(x => new CountryViewModel(x)).ToList(), "Id", "NameEn");
                ViewBag.Region = new SelectList(_regionRepo.GetAll().Select(x => new RegionViewModel(x)).ToList(), "Id", "NameEn");
                ViewBag.District = new SelectList(_districtRepo.GetAll().Select(x => new DistrictViewModel(x)).ToList(), "Id", "NameEn");

                return View();
            }
            return RedirectToAction("Register", "Account");
        }
    }
}