using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veelki.Core.IServices;
using Veelki.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veelki.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketWatchController : ControllerBase
    {
        private readonly IMarketWatchService _marketWatchService;
        public MarketWatchController(IMarketWatchService marketWatchService)
        {
            _marketWatchService = marketWatchService;
        }
        [HttpGet, Route("GetBetHistory")]
        public async Task<CommonReturnResponse> GetBetHistory(int SportId,int UserId)
        {
            return await _marketWatchService.GetBetHistory(SportId, UserId);
        }
    }
}
