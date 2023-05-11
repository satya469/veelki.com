using Veelki.Core.IServices;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Entities;
using Veelki.Data.Repository;
using Veelki.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veelki.Model.ViewModel;
using Veelki.Model.Model;

namespace Veelki.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly IBaseRepository _baseRepository;
        public CommonService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CommonReturnResponse> GetAllRolesAsync()
        {
            try
            {
                var roles = await _baseRepository.GetListAsync<UserRoles>();
                return new CommonReturnResponse
                {
                    Data = roles,
                    Message = roles.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = roles.Count > 0,
                    Status = roles.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAllSliderAsync()
        {
            try
            {
                var sliders = await _baseRepository.GetListAsync<Slider>();
                return new CommonReturnResponse
                {
                    Data = sliders,
                    Message = sliders.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sliders.Count > 0,
                    Status = sliders.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAllLogoAsync()
        {
            try
            {
                var logos = await _baseRepository.GetListAsync<Logo>();
                return new CommonReturnResponse
                {
                    Data = logos,
                    Message = logos.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = logos.Count > 0,
                    Status = logos.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetLogoAsync()
        {
            try
            {
                var logos = (await _baseRepository.GetListAsync<Logo>()).Where(s => s.Status == true).FirstOrDefault();
                return new CommonReturnResponse
                {
                    Data = logos,
                    Message = logos != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = logos != null,
                    Status = logos != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetNewsAsync()
        {
            try
            {
                var news = await _baseRepository.GetListAsync<News>();
                return new CommonReturnResponse
                {
                    Data = news,
                    Message = news.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = news.Count > 0,
                    Status = news.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetActivityLogAsync()
        {
            List<ActivityLogVM> activityLogVMs = null;
            try
            {
                string query = "select ActivityLog.*,Users.FullName as UserName from ActivityLog,Users where ActivityLog.UserId = Users.Id;";
                var result = await _baseRepository.GetQueryMultipleAsync(query, null, gr => gr.Read<ActivityLogVM>());
                activityLogVMs = (result[0] as List<ActivityLogVM>).ToList();
                return new CommonReturnResponse
                {
                    Data = activityLogVMs,
                    Message = activityLogVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = activityLogVMs.Count > 0,
                    Status = activityLogVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetUserActivityLogAsync(int UserId)
        {
            List<ActivityLogVM> activityLogVMs = null;
            try
            {
                string query = "select ActivityLog.*,Users.FullName as UserName from ActivityLog,Users where ActivityLog.UserId = Users.Id and Users.RoleId = 7 and UserId = " + UserId + ";";
                var result = await _baseRepository.GetQueryMultipleAsync(query, null, gr => gr.Read<ActivityLogVM>());
                activityLogVMs = (result[0] as List<ActivityLogVM>).ToList();
                return new CommonReturnResponse
                {
                    Data = activityLogVMs,
                    Message = activityLogVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = activityLogVMs.Count > 0,
                    Status = activityLogVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAccountStatementAsync(int UserId)
        {
            IDictionary<string, object> _keyValues = new Dictionary<string, object>();
            List<AccountStatementVM> accountStatementVMs = new List<AccountStatementVM>();
            try
            {
                var userList = await _baseRepository.GetListAsync<Users>();

                _keyValues.Add("ToUserId", UserId);
                _keyValues.Add("IsAccountStatement", true);
                var accountStatementList = await _baseRepository.SelectAsync<AccountStatement>(_keyValues);
                foreach (var item in accountStatementList)
                {
                    var accountStatementVM = new AccountStatementVM
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Deposit = item.Deposit,
                        Balance = item.Balance,
                        Withdraw = item.Withdraw,
                        Remark = item.Remark,
                        FromUser = userList.Where(x => x.Id == item.FromUserId).Select(y => y.FullName).FirstOrDefault(),
                        ToUser = userList.Where(x => x.Id == item.ToUserId).Select(y => y.FullName).FirstOrDefault()
                    };
                    accountStatementVMs.Add(accountStatementVM);
                }
                return new CommonReturnResponse
                {
                    Data = accountStatementVMs,
                    Message = accountStatementVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = accountStatementVMs.Count > 0,
                    Status = accountStatementVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAccountStatementForSuperAdminAsync(int AdminId)
        {
            IDictionary<string, object> _keyValues = new Dictionary<string, object>();
            List<AccountStatementVM> accountStatementVMs = new List<AccountStatementVM>();
            try
            {
                var userList = await _baseRepository.GetListAsync<Users>();

                _keyValues.Add("FromUserId", AdminId);
                _keyValues.Add("IsAccountStatement", true);
                var accountStatementList = await _baseRepository.SelectAsync<AccountStatement>(_keyValues);
                foreach (var item in accountStatementList)
                {
                    var accountStatementVM = new AccountStatementVM
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Deposit = item.Deposit,
                        Balance = item.Balance,
                        Withdraw = item.Withdraw,
                        Remark = item.Remark,
                        FromUser = userList.Where(x => x.Id == item.FromUserId).Select(y => y.FullName).FirstOrDefault(),
                        ToUser = userList.Where(x => x.Id == item.ToUserId).Select(y => y.FullName).FirstOrDefault()
                    };
                    accountStatementVMs.Add(accountStatementVM);
                }
                return new CommonReturnResponse
                {
                    Data = accountStatementVMs,
                    Message = accountStatementVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = accountStatementVMs.Count > 0,
                    Status = accountStatementVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetProfitAndLossAsync(int UserId)
        {
            List<ProfitAndLossVM> profitAndLossVMs = new List<ProfitAndLossVM>();
            try
            {
                string query = string.Format(@"select  Bets.Id,BetId,Sports.SportName,Event,Market,Selection,(CASE WHEN OddsType = 1 THEN 'Match Odds' ELSE Case WHEN OddsType = 2 THEN 'Bookmaker' ELSE 'Fancy' end END) as OddsType,Type,OddsRequest,AmountStake,PlaceTime,SettleTime,IsSettlement,(CASE WHEN ResultType = 1 THEN 'Win' ELSE Case WHEN ResultType = 2 THEN 'Loss' ELSE 'Draw' end END) as ResultType,ResultAmount from Bets,Sports where Bets.SportId = Sports.Id and Bets.Status = 1 and Bets.IsSettlement = 1 and UserId = {0}", UserId);

                var result = await _baseRepository.GetQueryMultipleAsync(query, new { UserId = UserId }, gr => gr.Read<ProfitAndLossVM>());
                profitAndLossVMs = (result[0] as List<ProfitAndLossVM>).ToList();
                return new CommonReturnResponse
                {
                    Data = profitAndLossVMs,
                    Message = profitAndLossVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = profitAndLossVMs.Count > 0,
                    Status = profitAndLossVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetRollingCommisionAsync(int ParentId, int UserId, int Type)
        {
            List<RollingCommisionVM> rollingCommisionVMList = new List<RollingCommisionVM>();
            RollingCommision rollingCommision = null;
            try
            {
                if (Type == 1)
                {
                    string query = string.Format(@"Select * from RollingCommision where ToUserId = {0}", UserId);
                    rollingCommision = (await _baseRepository.QueryAsync<RollingCommision>(query)).FirstOrDefault();
                    return new CommonReturnResponse
                    {
                        Data = rollingCommision,
                        Message = rollingCommision != null ? MessageStatus.Success : MessageStatus.NoRecord,
                        IsSuccess = rollingCommision != null,
                        Status = rollingCommision != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                    };
                }
                else
                {
                    var userList = await _baseRepository.GetListAsync<Users>();
                    string query = string.Format(@"Select * from RollingCommision where FromUserId = {0}", ParentId);
                    var rollingCommisionList = (await _baseRepository.QueryAsync<RollingCommision>(query)).ToList();
                    foreach (var item in rollingCommisionList)
                    {
                        var rollingCommisionVM = new RollingCommisionVM
                        {
                            Id = item.Id,
                            FromUserId = ParentId,
                            ToUserId = UserId,
                            FromUserName = userList.Where(x => x.Id == item.FromUserId).Select(y => y.FullName).FirstOrDefault(),
                            ToUserName = userList.Where(x => x.Id == item.ToUserId).Select(y => y.FullName).FirstOrDefault(),
                            Fancy = item.Fancy,
                            Matka = item.Matka,
                            Casino = item.Casino,
                            Binary = item.Binary,
                            Bookmaker = item.Bookmaker,
                            SportBook = item.SportBook
                        };
                        rollingCommisionVMList.Add(rollingCommisionVM);
                    }
                    return new CommonReturnResponse
                    {
                        Data = rollingCommisionVMList,
                        Message = rollingCommisionVMList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                        IsSuccess = rollingCommisionVMList.Count > 0,
                        Status = rollingCommisionVMList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                    };
                }
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetBetMarketListAsync(int UserId)
        {
            List<MarketVM> marketList = new List<MarketVM>();
            try
            {
                string query = string.Format(@"select distinct(Event),EventId from Bets where UserId = {0} and IsSettlement = 2 and isnull(ResultType,0) = 0", UserId);
                var bets = (await _baseRepository.QueryAsync<Bets>(query)).ToList();
                foreach (var item in bets)
                {
                    var market = new MarketVM
                    {
                        EventId = item.EventId,
                        Event = item.Event
                    };
                    marketList.Add(market);
                }
                return new CommonReturnResponse
                {
                    Data = marketList,
                    Message = marketList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = marketList.Count > 0,
                    Status = marketList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetBetEventListAsync(int sportId)
        {
            List<MarketVM> marketList = new List<MarketVM>();
            try
            {
                string query = string.Format(@"select distinct(Event),EventId from Bets where SportId = {0}", sportId);
                var bets = (await _baseRepository.QueryAsync<Bets>(query)).ToList();
                foreach (var item in bets)
                {
                    var market = new MarketVM
                    {
                        EventId = item.EventId,
                        Event = item.Event
                    };
                    marketList.Add(market);
                }
                return new CommonReturnResponse
                {
                    Data = marketList,
                    Message = marketList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = marketList.Count > 0,
                    Status = marketList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetOpenBetListAsync(int UserId, long EventId)
        {
            List<Bets> openBetList = new List<Bets>();
            string query = string.Empty;
            try
            {
                if (EventId > 0)
                {
                    query = string.Format(@"select * from Bets where UserId = {0} and IsSettlement = 2 and isnull(ResultType,0) = 0 and EventId = {1}", UserId, EventId);
                }
                else
                {
                    query = string.Format(@"select * from Bets where UserId = {0} and IsSettlement = 2 and isnull(ResultType,0) = 0", UserId);
                }
                
                openBetList = (await _baseRepository.QueryAsync<Bets>(query)).ToList();
                return new CommonReturnResponse
                {
                    Data = openBetList,
                    Message = openBetList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = openBetList.Count > 0,
                    Status = openBetList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
        public async Task<CommonReturnResponse> GetBetDataListAsync(long EventId)
        {
            IDictionary<string, object> _keyValues = new Dictionary<string, object>();
            List<Bets> openBetList = new List<Bets>();
            try
            {
                _keyValues.Add("EventId", EventId);
                openBetList = (await _baseRepository.SelectAsync<Bets>(_keyValues)).ToList();
                return new CommonReturnResponse
                {
                    Data = openBetList,
                    Message = openBetList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = openBetList.Count > 0,
                    Status = openBetList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetBetDataListByMarketIdAsync(string MarketId)
        {
            IDictionary<string, object> _keyValues = new Dictionary<string, object>();
            List<Bets> openBetList = new List<Bets>();
            try
            {
                _keyValues.Add("MarketId", MarketId);                
                openBetList = (await _baseRepository.SelectAsync<Bets>(_keyValues)).ToList();
                return new CommonReturnResponse
                {
                    Data = openBetList,
                    Message = openBetList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = openBetList.Count > 0,
                    Status = openBetList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }


        
    }
}
