using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Veelki.Core.IServices;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Entities;
using RB444.Model.Model;
using RB444.Model.ViewModel;
using Veelki.Models.Model;

namespace Veelki.Admin.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;

        public ReportController(IRequestServices requestServices, IConfiguration configuration)
        {
            _requestServices = requestServices;
            _configuration = configuration;
        }
        public async Task<IActionResult> RollingCommision()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null;
            if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            CommonReturnResponse commonModel = null;
            List<RollingCommisionVM> rollingCommisionVMs = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(string.Format("{0}Common/GetRollingCommission?PrentId={1}&UserId=13&Type=2", _configuration["ApiKeyUrl"], user.Id));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    rollingCommisionVMs = jsonParser.ParsJson<List<RollingCommisionVM>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(rollingCommisionVMs);
        }

        public async Task<IActionResult> SettlementData()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null;
            if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
            List<Bets> openBetList = new List<Bets>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetSports?type=2", _configuration["ApiKeyUrl"]));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    sportsDatalist = jsonParser.ParsJson<List<Sports>>(Convert.ToString(commonModel.Data));
                }
                ViewBag.SportsList = sportsDatalist;
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(openBetList);
        }

        [HttpPost]
        public async Task<ActionResult> GetBetEventData(int SportId)
        {
            CommonReturnResponse commonModel = null;
            List<MarketVM> eventList = new List<MarketVM>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetEventList?SportId={1}", _configuration["ApiKeyUrl"], SportId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    eventList = jsonParser.ParsJson<List<MarketVM>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return Json(eventList);
        }
        [HttpPost]
        public async Task<ActionResult> GetSettlementDataList(long EventId)
        {
            CommonReturnResponse commonModel = null;
            List<Bets> openBetList = new List<Bets>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetBetDataList?EventId={1}", _configuration["ApiKeyUrl"], EventId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    openBetList = jsonParser.ParsJson<List<Bets>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return PartialView("_SettlementList", openBetList);
        }

        public IActionResult BetDataList()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null;
            if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            UserBetPagination userBetPagination = new UserBetPagination();
            userBetPagination.betList = new List<Bets>();
            return View(userBetPagination);
        }

        [HttpPost]
        public async Task<ActionResult> GetBetDataList(UserBetsHistory model)
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null;
            if (user != null) { model.UserId = user.Id == 3 ? 8 : user.Id; } else { return RedirectToAction("Login", "Account"); }
            CommonReturnResponse commonModel = new CommonReturnResponse();
            UserBetPagination userBetPagination = new UserBetPagination();
            try
            {
                commonModel = await _requestServices.PostAsync<UserBetsHistory, CommonReturnResponse>(String.Format("{0}BetApi/GetBetHistory", _configuration["ApiKeyUrl"]), model);
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    userBetPagination = jsonParser.ParsJson<UserBetPagination>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return PartialView("_BetList", userBetPagination);
        }
    }
}
