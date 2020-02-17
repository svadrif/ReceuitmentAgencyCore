using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecruitmentAgencyCore.Models;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;

namespace RecruitmentAgencyCore.Controllers
{
    [Authorize]
    public class JobSeekerController : Controller
    {
        private readonly ILogger<JobSeekerController> _logger;
        private readonly IGenericRepository<User> _userRepository;
        public JobSeekerController(ILogger<JobSeekerController> logger, IGenericRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {  
            HttpContext.Session.SetString("UserName", HttpContext.User?.Identity?.Name);
            _logger.LogInformation($"User '{HttpContext.User?.Identity?.Name} logged in'");
            return View();
        }
    }
}