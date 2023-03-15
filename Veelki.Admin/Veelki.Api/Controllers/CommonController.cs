using Microsoft.AspNetCore.Mvc;
using Veelki.Core.IServices;
using Veelki.Models.Model;
using System.Threading.Tasks;

namespace Veelki.Api.Controllers
{
    [Route("api/Common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }       

        [HttpGet, Route("GetAllSliders")]
        public async Task<CommonReturnResponse> GetAllSliders()
        {
            return await _commonService.GetAllSliderAsync();
        }

        [HttpGet, Route("GetAllNews")]
        public async Task<CommonReturnResponse> GetAllNews()
        {
            return await _commonService.GetNewsAsync();
        }

        [HttpGet, Route("GetAllLogo")]
        public async Task<CommonReturnResponse> GetAllLogo()
        {
            return await _commonService.GetAllLogoAsync();
        }

        [HttpGet, Route("GetLogo")]
        public async Task<CommonReturnResponse> GetLogo()
        {
            return await _commonService.GetLogoAsync();
        }

        [HttpGet, Route("GetActivityLog")]
        public async Task<CommonReturnResponse> GetActivityLog()
        {
            return await _commonService.GetActivityLogAsync();
        }

        [HttpGet, Route("GetUserActivityLog")]
        public async Task<CommonReturnResponse> GetUserActivityLog(int UserId)
        {
            return await _commonService.GetUserActivityLogAsync(UserId);
        }

        [HttpGet, Route("GetAccountStatement")]
        public async Task<CommonReturnResponse> GetAccountStatement(int UserId)
        {
            return await _commonService.GetAccountStatementAsync(UserId);
        }

        [HttpGet, Route("GetAccountStatementForSuperAdmin")]
        public async Task<CommonReturnResponse> GetAccountStatementForSuperAdmin(int AdminId)
        {
            // not usable
            return await _commonService.GetAccountStatementForSuperAdminAsync(AdminId);
        }

        [HttpGet, Route("GetProfitAndLoss")]
        public async Task<CommonReturnResponse> GetProfitAndLoss(int UserId)
        {
            return await _commonService.GetProfitAndLossAsync(UserId);
        }

        [HttpGet, Route("GetRollingCommission")]
        public async Task<CommonReturnResponse> GetRollingCommission(int ParentId, int UserId, int Type)
        {
            return await _commonService.GetRollingCommisionAsync(ParentId, UserId, Type);
        }

        [HttpGet, Route("GetMarketList")]
        public async Task<CommonReturnResponse> GetMarketList(int UserId)
        {
            return await _commonService.GetBetMarketListAsync(UserId);
        }

        [HttpGet, Route("GetEventList")]
        public async Task<CommonReturnResponse> GetEventList(int SportId)
        {
            return await _commonService.GetBetEventListAsync(SportId);
        }

        [HttpGet, Route("GetOpenBetList")]
        public async Task<CommonReturnResponse> GetOpenBetList(int UserId, long EventId)
        {
            return await _commonService.GetOpenBetListAsync(UserId, EventId);
        }

        [HttpGet, Route("GetBetDataList")]
        public async Task<CommonReturnResponse> GetBetDataList(long EventId)
        {
            return await _commonService.GetBetDataListAsync(EventId);
        }
        [HttpGet, Route("GetBetDataListByMarketId")]
        public async Task<CommonReturnResponse> GetBetDataListByMarketId(string MarketId)
        {
            return await _commonService.GetBetDataListByMarketIdAsync(MarketId);
        }
    }
}
