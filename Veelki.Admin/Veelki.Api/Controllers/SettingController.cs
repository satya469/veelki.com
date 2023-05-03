using Microsoft.AspNetCore.Mvc;
using Veelki.Core.IServices;
using Veelki.Data.Entities;
using Veelki.Model.ViewModel;
using Veelki.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veelki.Api.Controllers
{
    [Route("api/Setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPost, Route("AddOrUpdateSportsSetting")]
        public async Task<CommonReturnResponse> AddOrUpdateSportsSetting(Sports sportsSetting)
        {
            return await _settingService.AddOrUpdateSportsSettingAsync(sportsSetting);
        }

        [HttpPost, Route("UpdateSeriesSetting")]
        public async Task<CommonReturnResponse> UpdateSeriesSetting(Series seriesSetting)
        {
            return await _settingService.UpdateSeriesSettingAsync(seriesSetting);
        }

        [HttpPost, Route("UpdateMatchSetting")]
        public async Task<CommonReturnResponse> UpdateMatchSetting(UpdateMatchVM matchSetting)
        {
            return await _settingService.UpdateMatchSettingAsync(matchSetting);
        }

        [HttpPost, Route("UpdateStakeLimit")]
        public async Task<CommonReturnResponse> UpdateStakeLimit(List<StakeLimit> stakeLimitList)
        {
            return await _settingService.UpdateStakeLimitAsync(stakeLimitList);
        }

        [HttpGet, Route("GetStakeLimit")]
        public async Task<CommonReturnResponse> GetStakeLimit()
        {
            return await _settingService.GetStakeLimitAsync();
        }
    }
}
