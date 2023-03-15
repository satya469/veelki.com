using Veelki.Models.Model;
using System.Threading.Tasks;

namespace Veelki.Core.IServices.BetfairApi
{
    public interface IBetfairApiServices
    {
        /// <summary>
        /// Get All Sports.
        /// </summary>
        /// <returns>Sports List<SportsData></returns>
        Task<CommonReturnResponse> GetSportsListAsync();

        /// <summary>
        /// Get Series list with specific sport
        /// </summary>
        /// <param name="SportName">Sport Name</param>
        /// <returns>Series List</returns>
        Task<CommonReturnResponse> GetSeriesListBySportsAsync(string SportName);

        /// <summary>
        /// Get match List
        /// </summary>
        /// <param name="Key">Match Key</param>
        /// <returns>Odds for bet</returns>
        Task<CommonReturnResponse> GetMatchListAsync(string Key);

        /// <summary>
        /// Get match Odds only betfair
        /// </summary>
        /// <param name="id">Match id</param>
        /// <param name="Key">Match Key</param>
        /// <returns>Odds for bet</returns>
        Task<CommonReturnResponse> GetMatchOddsAsync(string id, string Key);
    }
}
