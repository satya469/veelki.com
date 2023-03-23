using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Veelki.Admin.Models;
using Veelki.Data.Entities;
using System.Diagnostics;
using System.Threading.Tasks;
using Veelki.Models.Model;
using System.Collections.Generic;
using Veelki.Core.IServices;
using Microsoft.Extensions.Configuration;
using Veelki.Core.ServiceHelper;
using System;

namespace Veelki.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;

        public HomeController(IRequestServices requestServices, IConfiguration configuration, UserManager<Users> userManager)
        {
            _requestServices = requestServices;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            CommonReturnResponse commonModel = null;
            var model = new RegisterListVM();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetUsersByParentId?ParentId={1}&RoleId={2}&UserId={3}", _configuration["ApiKeyUrl"], user.Id, 3, 0));
                model = jsonParser.ParsJson<RegisterListVM>(Convert.ToString(commonModel.Data));
                ViewBag.roleIdBass = 3;
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
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
    }
}
