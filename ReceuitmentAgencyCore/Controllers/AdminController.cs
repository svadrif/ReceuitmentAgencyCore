using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;

namespace RecruitmentAgencyCore.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IGenericRepository<User> _userRepo;
        public AdminController(IGenericRepository<User> userRepo, IGenericRepository<Logging> loggingRepo) : base(userRepo, loggingRepo)
        {
            _userRepo = userRepo;
        }
        public IActionResult Statistics()
        {
            return View();
        }

        public IActionResult JobSeekers()
        {
            return View();
        }
    }
}