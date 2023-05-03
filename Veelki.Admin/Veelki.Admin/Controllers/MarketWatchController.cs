using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Veelki.Core.IServices;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Entities;
using Veelki.Model.Model;
using Veelki.Model.ViewModel;
using Veelki.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veelki.Admin.Controllers
{
    public class MarketWatchController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MarketWatchController(IRequestServices requestServices, IConfiguration configuration, UserManager<Users> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _requestServices = requestServices;
            _configuration = configuration;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: MarketWatch       
        public async Task<ActionResult> MarketWatch()
        {
            CommonReturnResponse commonModel = null;
            List<Sports> sportsDatalist = null;
            List<Matches> matcheslist = new List<Matches>();
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetSports?type=2", _configuration["ApiKeyUrl"]));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    sportsDatalist = jsonParser.ParsJson<List<Sports>>(Convert.ToString(commonModel.Data));
                }
                ViewBag.SportsList = sportsDatalist;
            }
            catch (Exception)
            {

                throw;
            }
            return View(matcheslist);
        }

        [HttpPost]
        public async Task<ActionResult> GetBetEventData(int SportId)
        {
            CommonReturnResponse commonModel = null;
            List<Series> serieslist = new List<Series>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetSeries?SportId={1}&type=2", _configuration["ApiKeyUrl"], SportId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    serieslist = jsonParser.ParsJson<List<Series>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return Json(serieslist);
        }
        [HttpPost]
        public async Task<ActionResult> GetSeriesMatchList(int SportId, long SeriesId)
        {
            CommonReturnResponse commonModel = null;
            List<Matches> matcheslist = new List<Matches>();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetMatches?SportId={1}&type=2&SeriesId={2}", _configuration["ApiKeyUrl"], SportId, SeriesId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    matcheslist = jsonParser.ParsJson<List<Matches>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return PartialView("_matchList", matcheslist);
        }

        [HttpGet]
        public async Task<ActionResult> TeamMarketWatch(string MarketId, long eventId, int SportId)
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            CommonReturnResponse commonModel = null;
            List<Bets> betsList = new List<Bets>();
            var eventModel = new EventModel();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetBetDataListByMarketId?MarketId={1}", _configuration["ApiKeyUrl"], MarketId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    betsList = jsonParser.ParsJson<List<Bets>>(Convert.ToString(commonModel.Data));
                }
                ViewBag.BetList = betsList;

                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetMatchOdds?MarketId={1}&eventId={2}&SportId={3}", _configuration["ApiKeyUrl"], MarketId, eventId, SportId));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    eventModel = jsonParser.ParsJson<EventModel>(Convert.ToString(commonModel.Data));
                    ViewBag.MatchOdds = eventModel.data.matchOddsData;
                }
                else
                {
                    ViewBag.MatchOdds = new List<MatchOddsData>();
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return View();
        }

        public ActionResult ManageSeries()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            return View();
        }


        public ActionResult IndianFancy()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            return View();
        }


        public ActionResult SessionFancy()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            return View();
        }


        public ActionResult BetfairMarket()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            return View();
        }
    }
}
