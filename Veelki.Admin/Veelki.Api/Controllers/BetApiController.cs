using Microsoft.AspNetCore.Mvc;
using Veelki.Core.IServices;
using Veelki.Data.Entities;
using RB444.Model.Model;
using Veelki.Models.Model;
using System.Threading.Tasks;

namespace Veelki.Api.Controllers
{
    [Route("api/BetApi")]
    [ApiController]
    public class BetApiController : ControllerBase
    {
        private readonly IBetApiService _betApiService;
        public BetApiController(IBetApiService betApiService)
        {
            _betApiService = betApiService;
        }

        [HttpPost, Route("SaveBets")]
        public async Task<CommonReturnResponse> SaveBets(Bets model)
        {
            return await _betApiService.SaveBets(model);
        }

        [HttpPost, Route("GetBetHistory")]
        public async Task<CommonReturnResponse> GetBetHistory(UserBetsHistory model)
        {
            return await _betApiService.GetBetHistoryAsync(model);
        }

        [HttpGet, Route("BetSettle")]
        public async Task<CommonReturnResponse> BetSettle()
        {
            return await _betApiService.BetSettleAsync();
        }

        [HttpGet, Route("GetBackAndLayBetAmount")]
        public async Task<CommonReturnResponse> GetBackAndLayBetAmount(int UserId, string marketId, int SportId)
        {
            return await _betApiService.GetBackAndLayAmountAsync(UserId, marketId, SportId);
        }
    }
}
