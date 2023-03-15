using Veelki.Data.Entities;
using RB444.Model.ViewModel;
using Veelki.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Veelki.Core.IServices
{
    public interface ISettingService
    {
        /// <summary>
        /// Add or update sports setting
        /// </summary>
        /// <param name="sportsSetting"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> AddOrUpdateSportsSettingAsync(Sports sportsSetting);

        /// <summary>
        /// Update Series Setting.
        /// </summary>
        /// <param name="seriesSetting"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateSeriesSettingAsync(Series seriesSetting);

        /// <summary>
        /// Update Match setting.
        /// </summary>
        /// <param name="matchSetting"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateMatchSettingAsync(UpdateMatchVM matchSetting);

        /// <summary>
        /// Update Stake Limit.
        /// </summary>
        /// <param name="stakeLimitList"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateStakeLimitAsync(List<StakeLimit> stakeLimitList);

        /// <summary>
        /// Get All Stake Limit.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetStakeLimitAsync();
    }
}
