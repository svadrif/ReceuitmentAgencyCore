using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using RecruitmentAgencyCore.Data.Models;
using RecruitmentAgencyCore.Data.Repository;

namespace RecruitmentAgencyCore.Controllers
{
    public class BaseController : Controller
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Logging> _loggingRepo;

        public BaseController(IGenericRepository<User> userRepo, IGenericRepository<Logging> loggingRepo)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _loggingRepo = loggingRepo ?? throw new ArgumentNullException(nameof(loggingRepo));
        }

        // For logging
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userName = context?.HttpContext?.User?.Identity?.Name;
            User user = _userRepo.Find(x => x.UserName == userName);

            string controllerName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName;
            string actionName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;
            string methodType = context.HttpContext.Request.Method;
            string ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();

            string type = string.Empty;
            string value = string.Empty;

            #region add
            if (actionName.ToLower().Contains("add") || actionName.ToLower().Contains("save") || actionName.ToLower().Contains("create")) { type = "ADD!"; }
            #endregion

            #region delete
            else if (actionName.ToLower().Contains("delete")) { type = "DELETE!"; }
            #endregion

            #region edit
            else if (actionName.ToLower().Contains("edit") || actionName.ToLower().Contains("update")) { type = "EDIT"; }
            #endregion

            if (methodType == "POST")
            {
                value = JsonConvert.SerializeObject(context.ActionArguments);
            }

            Logging logging = new Logging
            {
                UserId = user?.UserId,
                User = user,
                IpAddress = ipAddress,
                ActionTime = DateTime.Now,
                ControllerName = controllerName,
                ActionName = actionName,
                MethodType = methodType,
                Value = value,
                Type = type
            };

            _loggingRepo.Add(logging);

            base.OnActionExecuting(context);
        }
    }
}