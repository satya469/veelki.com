using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veelki.Core.IServices.BetfairApi;
using Veelki.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veelki.Api.Controllers.BetfairApi
{
    [Route("api/exchange")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet, Route("GetSports")]
        public async Task<CommonReturnResponse> GetSports(int type)
        {
            return await _exchangeService.GetSportsAsync(type);
        }

        [HttpGet, Route("GetSeries")]
        public async Task<CommonReturnResponse> GetSeries(int SportId, int type)
        {
            return await _exchangeService.GetSeriesListAsync(SportId, type);
        }

        [HttpGet, Route("GetMatchesOld")]
        public async Task<CommonReturnResponse> GetMatchesOld(int SportId, int SeriesId, int type)
        {
            return await _exchangeService.GetMatchesListAsync(SportId, SeriesId, type);
        }

        [HttpGet, Route("GetMatches")]
        public async Task<CommonReturnResponse> GetMatches(int SportId, int SeriesId, int type)
        {
            return await _exchangeService.GetMatchesListAsync(SportId, SeriesId, type);
        }

        [HttpGet, Route("GetMatchOdds")]
        public async Task<CommonReturnResponse> GetMatchOdds(string marketId, long eventId, int SportId)
        {
            return await _exchangeService.GetMatchEventsAsync(marketId, eventId, SportId);
        }

        [HttpGet, Route("GetSportEvents")]
        public async Task<CommonReturnResponse> GetSportEvents(int SportId)
        {
            return await _exchangeService.GetSportsEventsAsync(SportId);
        }

        [HttpGet, Route("GetSportsEventsForWindowService")]
        public async Task<CommonReturnResponse> GetSportsEventsForWindowService()
        {
            return await _exchangeService.GetSportsEventsForWindowServiceAsync();
        }

        [HttpGet, Route("GetInPlaySportEvents")]
        public async Task<CommonReturnResponse> GetInPlaySportEvents()
        {
            return await _exchangeService.GetSportsInPlayEventsAsync();
        }

        [HttpGet, Route("UpdateEventForPinned")]
        public async Task<CommonReturnResponse> UpdateEventForPinned(string marketId, bool isPinned)
        {
            return await _exchangeService.UpdatePinnedMatchAsync(marketId, isPinned);
        }
    }
}
