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
using static Veelki.Core.ServiceHelper.CommonFun;

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
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetUsersByParentId?ParentId={1}&RoleId={2}&UserId={3}&userStatus={4}", _configuration["ApiKeyUrl"], user.ParentId, user.RoleId + 1, 0, 0));
                model = jsonParser.ParsJson<RegisterListVM>(Convert.ToString(commonModel.Data));
                ViewBag.roleIdBass = 3;
                ViewBag.HeaderItem = HeaderItem.DownlineList;
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetUsersList(int UserStatus)
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            CommonReturnResponse commonModel = null;
            var model = new RegisterListVM();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetUsersByParentId?ParentId={1}&RoleId={2}&UserId={3}&userStatus={4}", _configuration["ApiKeyUrl"], user.ParentId, user.RoleId + 1, 0, UserStatus));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    model = jsonParser.ParsJson<RegisterListVM>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return PartialView("_DownlineList", model);
        }

        public async Task<JsonResult> DeleteUser(int UserId, int Status)
        {
            CommonReturnResponse commonModel = null;
            try
            {
                int statusId = Status; // Status.ToLower() == "active" ? 1 : Status.ToLower() == "suspend" ? 2 : 3;
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/UpdateUserStatus?Status={1}&UserId={2}", _configuration["ApiKeyUrl"], statusId, UserId));
                return Json(commonModel);
            }
            catch (Exception ex)
            {
                commonModel = new CommonReturnResponse()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message,
                    Status = ResponseStatusCode.NOTACCEPTABLE
                };
                return Json(JsonConvert.SerializeObject(commonModel));
            }
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
