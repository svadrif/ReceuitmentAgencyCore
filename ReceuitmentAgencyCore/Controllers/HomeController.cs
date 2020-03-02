using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;
using RecruitmentAgencyCore.Models;

namespace RecruitmentAgencyCore.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Culture> _cultureRepo;
        private readonly List<Culture> cultures;
        public HomeController(ILogger<HomeController> logger, IGenericRepository<User> userRepo, IGenericRepository<Culture> cultureRepo,
            IGenericRepository<Logging> loggingRepo) : base(userRepo, loggingRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
            _cultureRepo = cultureRepo;
            cultures = _cultureRepo.GetAll().ToList();
        }

        public IActionResult Index()
        {
            ViewBag.Cultures = cultures;
            ViewBag.CurrentCulture = (HttpContext?.Session?.GetString("culture") ?? "uz").ToUpper();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult ChangeLanguage(string culture)
        {
            HttpContext.Session.SetString("culture", culture);
            return RedirectToAction("Index");
        }
    }
}
