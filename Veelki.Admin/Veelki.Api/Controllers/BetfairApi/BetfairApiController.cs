using Microsoft.AspNetCore.Mvc;
using Veelki.Core.IServices.BetfairApi;
using Veelki.Models.Model;
using System.Threading.Tasks;

namespace Veelki.Api.Controllers.BetfairApi
{
    [Route("api/BetfairApi/BetfairApi")]
    [ApiController]
    public class BetfairApiController : ControllerBase
    {
        private readonly IBetfairApiServices _betfairApiServices;

        public BetfairApiController(IBetfairApiServices betfairApiServices)
        {
            _betfairApiServices = betfairApiServices;
        }


        [HttpGet, Route("GetSportsList")]
        public async Task<CommonReturnResponse> GetSportsList()
        {
            return await _betfairApiServices.GetSportsListAsync();
        }

        [HttpGet, Route("GetMatchList")]
        public async Task<CommonReturnResponse> GetMatchList(string Key)
        {
            return await _betfairApiServices.GetMatchListAsync(Key);
        }

        [HttpGet, Route("GetMatchOdds")]
        public async Task<CommonReturnResponse> GetMatchOdds(string id, string Key)
        {
            return await _betfairApiServices.GetMatchOddsAsync(id, Key);
        }
    }
}
