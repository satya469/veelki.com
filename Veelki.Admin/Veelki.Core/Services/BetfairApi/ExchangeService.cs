using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Veelki.Core.IServices;
using Veelki.Core.IServices.BetfairApi;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Entities;
using Veelki.Data.Repository;
using Veelki.Model.Model;
using Veelki.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Veelki.Core.ServiceHelper.CommonFun;

namespace Veelki.Core.Services.BetfairApi
{
    public class ExchangeService : IExchangeService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        CommonFun commonFun = new CommonFun();

        public ExchangeService(IBaseRepository baseRepository, IRequestServices requestServices, IConfiguration configuration)
        {
            _baseRepository = baseRepository;
            _requestServices = requestServices;
            _configuration = configuration;
        }

        public async Task<CommonReturnResponse> GetSportsAsync(int type)
        {
            IDictionary<string, object> _keyValues = null;
            var sports = new List<Sports>();
            try
            {
                if (type == 2)
                {
                    sports = await _baseRepository.GetListAsync<Sports>();
                }
                else
                {
                    _keyValues = new Dictionary<string, object> { { "Status", 1 } };
                    sports = (await _baseRepository.SelectAsync<Sports>(_keyValues)).ToList();
                }
                return new CommonReturnResponse
                {
                    Data = sports,
                    Message = sports.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sports.Count > 0,
                    Status = sports.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetSeriesListAsync(int SportId, int type)
        {
            IDictionary<string, object> _keyValues = null;
            var seriesReturnResponse = new SeriesReturnResponse();
            List<SeriesDataByApi> seriesDataByApis = null;
            List<SeriesDataByApi> seriesDistinctDataByApis = new List<SeriesDataByApi>();
            List<Series> serieslistByDatabase = null;
            List<Series> serieslist = new List<Series>();
            try
            {
                seriesReturnResponse = await _requestServices.GetAsync<SeriesReturnResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                seriesDataByApis = seriesReturnResponse.data;
                foreach (var itemSeries in seriesDataByApis)
                {
                    if (!seriesDistinctDataByApis.Any(x => x.SeriesId == itemSeries.SeriesId))
                        seriesDistinctDataByApis.Add(itemSeries);
                }
                _keyValues = new Dictionary<string, object> { { "SportId", SportId } };
                serieslistByDatabase = (await _baseRepository.SelectAsync<Series>(_keyValues)).ToList();

                foreach (var item in seriesDistinctDataByApis)
                {
                    var series = new Series
                    {
                        tournamentId = Convert.ToInt64(item.SeriesId),
                        tournamentName = item.SeriesName,
                        SportId = SportId,
                        Status = serieslistByDatabase.Count > 0 ? serieslistByDatabase.Where(x => x.tournamentId == item.SeriesId).FirstOrDefault() != null ? serieslistByDatabase.Where(x => x.tournamentId == item.SeriesId).Select(s => s.Status).FirstOrDefault() : true : true
                    };
                    serieslist.Add(series);

                    if (serieslistByDatabase.Count > 0 && type == 1)
                    {
                        foreach (var item2 in serieslistByDatabase)
                        {
                            if (item.SeriesName.Equals(item2.tournamentName) && item2.Status != true)
                            {
                                var itemToRemove = serieslist.FirstOrDefault(r => r.tournamentId == item2.tournamentId);
                                serieslist.Remove(itemToRemove);
                            }
                        }
                    }
                }

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
            finally { if (seriesDataByApis != null) { seriesDataByApis = null; } }
        }

        public async Task<CommonReturnResponse> GetMatchesListAsync(int SportId, long SeriesId, int type)
        {
            IDictionary<string, object> _keyValues = null;
            var matchReturnResponse = new MatchesReturnResponse();
            List<SportsEvent> matcheslist = new List<SportsEvent>();
            try
            {
                _keyValues = new Dictionary<string, object> { { "eid", SportId }, { "tournamentId", SeriesId } };
                matcheslist = (await _baseRepository.SelectAsync<SportsEvent>(_keyValues)).ToList();
                if (type == 1)
                {
                    matcheslist = matcheslist.Where(x => x.DisableMatch == false).ToList();
                }

                return new CommonReturnResponse
                {
                    Data = matcheslist,
                    Message = matcheslist.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = matcheslist.Count > 0,
                    Status = matcheslist.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (matcheslist != null) { matcheslist = null; } }
        }

        public async Task<CommonReturnResponse> GetMatchEventsAsync(string marketId, long eventId, int SportId)
        {
            var matchReturnResponse = new List<MatchReturnResponseNew>();
            var teamNameResponse = new TeamNameResponse();
            var eventModel = new EventModel();
            var teamSelectionIds = new List<TeamSelectionId>();
            eventModel.data = new Veelki.Models.Model.Data();
            eventModel.data.matchOddsData = new List<MatchOddsData>();
            List<MatchOddsData> matchOddsDataList = new List<MatchOddsData>();
            Price price = new Price();
            try
            {
                matchReturnResponse = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}{1}", _configuration["ApiMatchOddsUrl"], marketId));
                if (matchReturnResponse == null)
                {
                    eventModel.data.matchOddsData = null;
                    return new CommonReturnResponse
                    {
                        Data = eventModel,
                        Message = MessageStatus.NoRecord,
                        IsSuccess = false,
                        Status = ResponseStatusCode.NOTFOUND
                    };
                }

                teamNameResponse = await _requestServices.GetAsync<TeamNameResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                var runnerNames = teamNameResponse.data.Where(x => x.marketId == marketId).FirstOrDefault();
                if (runnerNames == null)
                {
                    eventModel.data.matchOddsData = null;
                    return new CommonReturnResponse
                    {
                        Data = eventModel,
                        Message = MessageStatus.NoRecord,
                        IsSuccess = false,
                        Status = ResponseStatusCode.NOTFOUND
                    };
                }
                teamSelectionIds = commonFun.GetTeamName(runnerNames);

                List<RunnerOld> runnerList = new List<RunnerOld>();
                foreach (var item in matchReturnResponse[0].runners)
                {
                    List<Back> backList = new List<Back>();
                    List<Lay> layList = new List<Lay>();
                    backList.Add(new Back
                    {
                        price = item.ex.availableToBack[2].price,
                        size = item.ex.availableToBack[2].size
                    });
                    backList.Add(new Back
                    {
                        price = item.ex.availableToBack[1].price,
                        size = item.ex.availableToBack[1].size
                    });
                    backList.Add(new Back
                    {
                        price = item.ex.availableToBack[0].price,
                        size = item.ex.availableToBack[0].size
                    });

                    layList.Add(new Lay
                    {
                        price = item.ex.availableToLay[0].price,
                        size = item.ex.availableToLay[0].size
                    });
                    layList.Add(new Lay
                    {
                        price = item.ex.availableToLay[1].price,
                        size = item.ex.availableToLay[1].size
                    });
                    layList.Add(new Lay
                    {
                        price = item.ex.availableToLay[2].price,
                        size = item.ex.availableToLay[2].size
                    });

                    runnerList.Add(new RunnerOld
                    {
                        selectionId = item.selectionId,
                        runnerName = teamSelectionIds.Where(x => x.selectionId == item.selectionId).Select(y => y.teamName).FirstOrDefault(),
                        handicap = 0,
                        status = item.status,
                        price = new Price
                        {
                            back = backList,
                            lay = layList
                        }
                    });
                }

                matchOddsDataList.Add(new MatchOddsData
                {
                    exEventId = eventId.ToString(),
                    eventName = runnerNames.eventName,
                    marketId = marketId,
                    exMarketId = marketId,
                    isSettlement = 0,
                    isScore = false,
                    isVoid = 0,
                    marketName = runnerNames.marketName,
                    marketType = runnerNames.marketType,
                    max = 25000,
                    min = 100,
                    tableFlag = runnerNames.marketType,
                    oddsType = runnerNames.marketType,
                    oddsData = new OddsData
                    {
                        betDelay = 5,
                        status = matchReturnResponse[0].status,
                        totalMatched = matchReturnResponse[0].totalMatched,
                        runners = runnerList
                    },
                });

                eventModel.data.matchOddsData = matchOddsDataList;
                eventModel.data.inPlay = matchReturnResponse[0].inplay;

                return new CommonReturnResponse
                {
                    Data = eventModel,
                    Message = MessageStatus.Success,
                    IsSuccess = true,
                    Status = ResponseStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            //finally { if (matchDataByApis != null) { matchDataByApis = null; } }
        }

        public async Task<CommonReturnResponse> GetSportsEventsAsync(int SportId)
        {
            //string sportNameForApi = "";
            var matchReturnResponse = new MatchesReturnResponse();
            var matchReturnResponseNew = new List<MatchReturnResponseNew>();

            var sportsEventList = new List<SportsEvent>();
            var sportsEventInPlayList = new List<SportsEvent>();
            var sportsEventNotInPlayList = new List<SportsEvent>();
            var sportsEventModelList = new List<SportsEventModel>();
            IDictionary<string, object> _keyValues = null;
            var sportList = new List<Sports>();
            try
            {
                if (SportId <= 0)
                {
                    _keyValues = new Dictionary<string, object>();
                    _keyValues.Add("Status", 1);
                    sportList = (await _baseRepository.SelectAsync<Sports>(_keyValues)).ToList();
                    for (int i = 0; i < sportList.Count; i++)
                    {
                        _keyValues = new Dictionary<string, object>();
                        _keyValues.Add("eid", sportList[i].Id.ToString());
                        sportsEventList.AddRange(await _baseRepository.SelectAsync<SportsEvent>(_keyValues));
                    }
                }
                else
                {
                    _keyValues = new Dictionary<string, object>();
                    _keyValues.Add("eid", SportId.ToString());
                    sportsEventList.AddRange(await _baseRepository.SelectAsync<SportsEvent>(_keyValues));
                }

                sportsEventList = sportsEventList.Where(x => x.DisableMatch == false).ToList();
                sportsEventInPlayList = sportsEventList.Where(x => x.inPlay == "True").ToList();
                sportsEventNotInPlayList = sportsEventList.Where(x => x.inPlay == "False").ToList();
                sportsEventNotInPlayList = sportsEventNotInPlayList.OrderBy(z => z.eventDate).ToList();
                sportsEventList = new List<SportsEvent>();
                sportsEventList.AddRange(sportsEventInPlayList);
                sportsEventList.AddRange(sportsEventNotInPlayList);
                return new CommonReturnResponse
                {
                    Data = sportsEventList,
                    Message = sportsEventList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sportsEventList.Count > 0,
                    Status = sportsEventList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse
                {
                    Data = null,
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                    IsSuccess = false,
                    Status = ResponseStatusCode.EXCEPTION
                };
            }
            finally { if (sportsEventModelList != null) { sportsEventModelList = null; } }
        }

        public async Task<CommonReturnResponse> GetSportsEventsForWindowServiceAsync()
        {
            //string sportNameForApi = "";
            var matchReturnResponse = new MatchesReturnResponse();
            var matchReturnResponseNew = new List<MatchReturnResponseNew>();

            var sportsEventList = new List<SportsEvent>();
            IDictionary<string, object> _keyValues = null;
            var sportList = new List<Sports>();
            var pinnedEventList = new List<SportsEvent>();
            var seriesDisableMatchList = new List<SportsEvent>();
            try
            {
                _keyValues = new Dictionary<string, object>();
                _keyValues.Add("IsPinnedMatch", true);
                pinnedEventList = (await _baseRepository.SelectAsync<SportsEvent>(_keyValues)).ToList();

                _keyValues = new Dictionary<string, object>();
                _keyValues.Add("DisableMatch", true);
                seriesDisableMatchList = (await _baseRepository.SelectAsync<SportsEvent>(_keyValues)).ToList();

                await _baseRepository.QueryAsync<SportsEvent>("delete from SportsEvent");

                _keyValues = new Dictionary<string, object>();
                _keyValues.Add("Status", 1);
                sportList = (await _baseRepository.SelectAsync<Sports>(_keyValues)).ToList();
                for (int i = 0; i < sportList.Count; i++)
                {
                    matchReturnResponse = await _requestServices.GetAsync<MatchesReturnResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], sportList[i].Id));
                    foreach (var item in matchReturnResponse.data)
                    {
                        matchReturnResponseNew = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}{1}", _configuration["ApiMatchOddsUrl"], item.marketId));
                        if (matchReturnResponseNew != null)
                        {
                            var sportsEventModel = new SportsEvent
                            {
                                gameId = item.eventId.ToString(),
                                eventName = item.eventName,
                                tournamentId = item.SeriesId,
                                eventDate = GetISTDateTime(item.eventDate),
                                marketId = item.marketId,
                                inPlay = matchReturnResponseNew[0].inplay.ToString(),
                                totalMatched = matchReturnResponseNew[0].totalMatched,
                                back11 = matchReturnResponseNew[0].runners[0].ex.availableToBack[0].price,
                                back1 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToBack[0].price : 0,
                                back12 = matchReturnResponseNew[0].runners[1].ex.availableToBack[0].price,
                                lay11 = matchReturnResponseNew[0].runners[0].ex.availableToLay[0].price,
                                lay1 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToLay[0].price : 0,
                                lay12 = matchReturnResponseNew[0].runners[1].ex.availableToLay[0].price,
                                eid = sportList[i].Id.ToString(),
                                m1 = "",
                                f = "",
                                tv = item.MainTvurl,
                                vir = 0,
                                IsPinnedMatch = pinnedEventList.Where(x => x.marketId == item.marketId).Select(y => y.IsPinnedMatch).FirstOrDefault() == true ? true : false,
                                DisableMatch = seriesDisableMatchList.Where(x => x.marketId == item.marketId).Select(y => y.DisableMatch).FirstOrDefault() == true ? true : false
                            };
                            sportsEventList.Add(sportsEventModel);
                        }
                    }
                }
                await _baseRepository.BulkInsert(sportsEventList);
                _baseRepository.Commit();

                return new CommonReturnResponse
                {
                    Data = sportsEventList.OrderByDescending(y => y.inPlay).ToList(),
                    Message = sportsEventList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sportsEventList.Count > 0,
                    Status = sportsEventList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse
                {
                    Data = null,
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                    IsSuccess = false,
                    Status = ResponseStatusCode.EXCEPTION
                };
            }
            finally { if (sportsEventList != null) { sportsEventList = null; } }
        }

        public async Task<CommonReturnResponse> GetSportsInPlayEventsAsync()
        {
            var sportsEventList = new List<SportsEvent>();
            var sportsInPlayEventList = new SportInPlayEventModelNew();
            var matchReturnResponse = new MatchesReturnResponse();
            var matchReturnResponseNew = new List<MatchReturnResponseNew>();
            IDictionary<string, object> _keyValues = null;
            var sportList = new List<Sports>();
            try
            {
                _keyValues = new Dictionary<string, object>();
                _keyValues.Add("Status", 1);
                sportList = (await _baseRepository.SelectAsync<Sports>(_keyValues)).ToList();
                for (int i = 0; i < sportList.Count; i++)
                {
                    _keyValues = new Dictionary<string, object>();
                    _keyValues.Add("eid", sportList[i].Id.ToString());
                    sportsEventList.AddRange(await _baseRepository.SelectAsync<SportsEvent>(_keyValues));
                }

                sportsEventList = sportsEventList.Where(x => x.DisableMatch == false).ToList();
                sportsInPlayEventList.sportsEventModelInPlay = sportsEventList.Where(x => Convert.ToBoolean(x.inPlay)).ToList();

                sportsInPlayEventList.sportsEventModelToday = sportsEventList.Where(x => x.eventDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).OrderByDescending(y => y.inPlay).ToList();

                sportsInPlayEventList.sportsEventModelTommorow = sportsEventList.Where(x => x.eventDate.ToString("dd/MM/yyyy") == DateTime.Now.AddDays(1).ToString("dd/MM/yyyy")).OrderByDescending(y => y.inPlay).ToList();

                return new CommonReturnResponse
                {
                    Data = sportsInPlayEventList,
                    Message = sportsInPlayEventList != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sportsInPlayEventList != null,
                    Status = sportsInPlayEventList != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (sportsEventList != null) { sportsEventList = null; } }
        }

        public async Task<CommonReturnResponse> UpdatePinnedMatchAsync(string marketId, bool isPinned)
        {
            try
            {
                string sql = string.Format(@"Update SportsEvent set IsPinnedMatch = {0} where marketId = '{1}'", isPinned == true ? 1 : 0, marketId);
                var _result = await _baseRepository.QueryAsync<SportsEvent>(sql);
                _baseRepository.Commit();
                return new CommonReturnResponse { Data = true, Message = "Match Pinned successfully.", IsSuccess = true, Status = ResponseStatusCode.OK };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }

        }
    }
}
