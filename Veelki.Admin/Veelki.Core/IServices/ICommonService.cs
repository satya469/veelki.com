using Veelki.Models.Model;
using System.Threading.Tasks;

namespace Veelki.Core.IServices
{
    public interface ICommonService
    {
        /// <summary>
        /// Get All user Roles.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllRolesAsync();

        /// <summary>
        /// Get All Slider.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllSliderAsync();

        /// <summary>
        /// Get News.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetNewsAsync();

        /// <summary>
        /// Get All Logo.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllLogoAsync();

        /// <summary>
        /// Get Active Logo.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetLogoAsync();

        /// <summary>
        /// Get All Role Activity Log.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetActivityLogAsync();

        /// <summary>
        /// Get User Activity Log.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetUserActivityLogAsync(int UserId);

        /// <summary>
        /// Get Account Statement.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAccountStatementAsync(int UserId);

        /// <summary>
        /// Get Account Statement for all user who are below to super admin.
        /// </summary>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAccountStatementForSuperAdminAsync(int AdminId);

        /// <summary>
        /// Get Profit and loss data of user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetProfitAndLossAsync(int UserId);

        /// <summary>
        /// Get rolling commission.
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="UserId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetRollingCommisionAsync(int ParentId, int UserId, int Type);

        /// <summary>
        /// Get market list.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBetMarketListAsync(int UserId);

        /// <summary>
        /// Get event list.
        /// </summary>
        /// <param name="sportId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBetEventListAsync(int sportId);

        /// <summary>
        /// Get Open bet list.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="EventId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetOpenBetListAsync(int UserId, long EventId);

        /// <summary>
        /// Get bet data list.
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBetDataListAsync(long EventId);

        /// <summary>
        /// Get bet data list.
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBetDataListByMarketIdAsync(string MarketId);
        
    }
}
