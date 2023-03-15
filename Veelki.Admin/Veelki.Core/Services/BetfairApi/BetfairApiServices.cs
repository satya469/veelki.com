using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Veelki.Core.IServices;
using Veelki.Core.IServices.BetfairApi;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Repository;
using Veelki.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veelki.Core.Services.BetfairApi
{
    public class BetfairApiServices : IBetfairApiServices
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;

        public BetfairApiServices(IBaseRepository baseRepository, IRequestServices requestServices, IConfiguration configuration)
        {
            _baseRepository = baseRepository;
            _requestServices = requestServices;
            _configuration = configuration;
        }

        public async Task<CommonReturnResponse> GetSportsListAsync()
        {
            CommonReturnResponse commonModel = null;
            var sportslist = new List<SportsSettings>();
            List<CommonModel> commonVMList = new List<CommonModel>();
            try
            {
                commonModel = await _requestServices.PostAsync<SportsSettings, CommonReturnResponse>("https://dream444.com/api/exchange/sports/sportsList", null);
                sportslist = jsonParser.ParsJson<List<SportsSettings>>(Convert.ToString(commonModel.Data));
                foreach (var item in sportslist)
                {
                    var commonVM = new CommonModel();
                    commonVM.Id = Convert.ToInt32(item.sportId);
                    commonVM.Name = item.sportName;
                    commonVMList.Add(commonVM);
                }
                return new CommonReturnResponse
                {
                    Data = sportslist,
                    Message = sportslist.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sportslist.Count > 0,
                    Status = sportslist.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse
                {
                    Data = null,
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                    IsSuccess = false,
                    Status = ResponseStatusCode.EXCEPTION
                };
            }
            finally { if (sportslist != null) { sportslist = null; } }
        }

        public async Task<CommonReturnResponse> GetSeriesListBySportsAsync(string SportName)
        {
            List<SeriesDataByApi> serieslist = null;
            try
            {
                serieslist = await _requestServices.GetAsync<List<SeriesDataByApi>>(string.Format("{0}?apiKey={1}", _configuration["ApiKeyUrl"], _configuration["ApiKey"]));
                serieslist = serieslist.ToList();
                return new CommonReturnResponse
                {
                    Data = serieslist,
                    Message = serieslist.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = serieslist.Count > 0,
                    Status = serieslist.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (serieslist != null) { serieslist = null; } }
        }

        public async Task<CommonReturnResponse> GetMatchListAsync(string Key)
        {
            List<MatchList> matchLists = null;
            try
            {
                matchLists = await _requestServices.GetAsync<List<MatchList>>(string.Format("{0}{1}/odds?regions=us&apiKey={2}", _configuration["ApiKeyUrl"], Key, _configuration["ApiKey"]));

                return new CommonReturnResponse
                {
                    Data = matchLists,
                    Message = matchLists.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = matchLists.Count > 0,
                    Status = matchLists.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (matchLists != null) { matchLists = null; } }
        }

        public async Task<CommonReturnResponse> GetMatchOddsAsync(string id, string Key)
        {
            List<MatchOdds> matchOdds = null;
            List<MatchOdds> modifyMatchOdds = new List<MatchOdds>();
            try
            {
                matchOdds = await _requestServices.GetAsync<List<MatchOdds>>(string.Format("{0}{1}/odds?regions=us&apiKey={2}", _configuration["ApiKeyUrl"], Key, _configuration["ApiKey"]));
                matchOdds = matchOdds.Where(x => x.id == id).ToList();

                foreach (var item in matchOdds)
                {
                    foreach (var item2 in item.bookmakers)
                    {
                        if (item2.key == "betfair")
                        {
                            var matchOdd = new MatchOdds
                            {
                                id = item.id,
                                sport_key = item.sport_key,
                                sport_title = item.sport_title,
                                commence_time = item.commence_time,
                                home_team = item.home_team,
                                away_team = item.away_team,
                                bookmakers = new List<Bookmaker>()
                                {
                                    new Bookmaker
                                    {
                                        key = item2.key,
                                        title = item2.title,
                                        last_update = item2.last_update,
                                        markets = item2.markets
                                    }
                                }
                            };
                            modifyMatchOdds.Add(matchOdd);
                        }
                    }
                }

                return new CommonReturnResponse
                {
                    Data = modifyMatchOdds,
                    Message = modifyMatchOdds.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = modifyMatchOdds.Count > 0,
                    Status = modifyMatchOdds.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (matchOdds != null) { matchOdds = null; } }
        }
    }
}
