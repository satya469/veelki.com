using Veelki.Data.Entities;
using Veelki.Models.Model;
using System.Threading.Tasks;
using Veelki.Model.Model;

namespace Veelki.Core.IServices
{
    public interface IBetApiService
    {
        /// <summary>
        /// Save Bets.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> SaveBets(Bets model);

        /// <summary>
        /// Get User bet history.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBetHistoryAsync(UserBetsHistory model);

        /// <summary>
        /// Bet settle after match finish.
        /// </summary>        
        /// <returns></returns>
        Task<CommonReturnResponse> BetSettleAsync();

        /// <summary>
        /// Get back or lay amount.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="marketId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBackAndLayAmountAsync(int UserId, string marketId, int SportId);
    }
}
