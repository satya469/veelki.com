using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Veelki.Core.IServices;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Entities;
using Veelki.Data.Repository;
using RB444.Model.Model;
using RB444.Model.ViewModel;
using Veelki.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veelki.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        CommonFun commonFun = new CommonFun();
        string teamAmountStr = "";

        public AccountService(IBaseRepository baseRepository, IRequestServices requestServices, IConfiguration configuration)
        {
            _baseRepository = baseRepository;
            _requestServices = requestServices;
            _configuration = configuration;
        }

        public async Task<CommonReturnResponse> GetOpeningBalanceAsync(int UserId)
        {
            double openingBalance = 0;
            double unsettleBetOpening = 0;
            try
            {
                string query = string.Format(@"select top 1 *  from AccountStatement where ToUserId = {0} order by id desc", UserId);
                var balance = (await _baseRepository.QueryAsync<AccountStatement>(query)).Select(x => x.Balance).FirstOrDefault();

                query = "select distinct marketid,SportId from Bets where IsSettlement = 2 and UserId = " + UserId;
                var betMarketList = (await _baseRepository.QueryAsync<Bets>(query)).ToList();
                for (int i = 0; i < betMarketList.Count; i++)
                {
                    unsettleBetOpening = unsettleBetOpening + await GetBackAndLayOpeningAndExposureAmountAsync(UserId, betMarketList[i].MarketId, betMarketList[i].SportId);
                }
                //query = string.Format(@"select * from Bets where IsSettlement <> 1 and UserId = {0}", UserId);
                //var betBackAmountList = (await _baseRepository.QueryAsync<Bets>(query)).Select(x => x.AmountStake).FirstOrDefault();

                //query = string.Format(@"select sum(AmountStake) as AmountStake from Bets where IsSettlement <> 1 and UserId = {0} and Type='back'", UserId);
                //var betBackAmountList = (await _baseRepository.QueryAsync<Bets>(query)).Select(x => x.AmountStake).FirstOrDefault();
                ////totalBetAmount = totalBetAmount + betAmountList;
                ////openingBalance = balance - betBackAmountList;

                //query = string.Format(@"select sum((OddsRequest * AmountStake)-AmountStake) as AmountStake from Bets where IsSettlement <> 1 and UserId = {0} and Type='lay'", UserId);
                //var betlayAmountList = (await _baseRepository.QueryAsync<Bets>(query)).Select(x => x.AmountStake).FirstOrDefault();

                //query = "select distinct marketid,AmountStake,selectionid, ((AmountStake * OddsRequest) - AmountStake) as AmountStake from Bets where Type = 'back' and IsSettlement = 2 and UserId = 12 order by SelectionId";
                //var betBackAmountList = (await _baseRepository.QueryAsync<Bets>(query)).ToList();

                //query = "select distinct marketid,AmountStake,selectionid, ((AmountStake * OddsRequest) - AmountStake) as AmountStake from Bets where Type = 'lay' and IsSettlement = 2 and UserId = 12 order by SelectionId";
                //var betlayAmountList = (await _baseRepository.QueryAsync<Bets>(query)).ToList();

                openingBalance = balance - Math.Abs(unsettleBetOpening);

                query = string.Format(@"select sum(ResultAmount) as ResultAmount from Bets where IsSettlement = 1 and UserId = {0}", UserId);
                var settleBetAmountList = (await _baseRepository.QueryAsync<Bets>(query)).Select(x => x.ResultAmount).FirstOrDefault();

                if (settleBetAmountList == null)
                {
                    settleBetAmountList = 0;
                }

                openingBalance = (double)(openingBalance + settleBetAmountList);
                openingBalance = Math.Truncate(100 * openingBalance) / 100;
                return new CommonReturnResponse
                {
                    Data = openingBalance,
                    Message = MessageStatus.Success,
                    IsSuccess = true,
                    Status = ResponseStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetBetExposureStackAsync(int UserId)
        {
            double unsettleBetExposure = 0;
            string query = string.Empty;
            try
            {
                //string query = string.Format(@"select sum(AmountStake) as AmountStake from Bets where IsSettlement <> 1 and UserId = {0} and Type='back'", UserId);
                //var exposureStackBack = (await _baseRepository.QueryAsync<Bets>(query)).Select(x => x.AmountStake).FirstOrDefault();

                query = "select distinct marketid,SportId from Bets where IsSettlement = 2 and UserId = " + UserId;
                var betMarketList = (await _baseRepository.QueryAsync<Bets>(query)).ToList();
                for (int i = 0; i < betMarketList.Count; i++)
                {
                    unsettleBetExposure = unsettleBetExposure + await GetBackAndLayOpeningAndExposureAmountAsync(UserId, betMarketList[i].MarketId, betMarketList[i].SportId);
                }
                //query = string.Format(@"select sum((OddsRequest * AmountStake)-AmountStake) as AmountStake from Bets where IsSettlement <> 1 and UserId = {0} and Type='lay'", UserId);
                //var exposureStackLay = (await _baseRepository.QueryAsync<Bets>(query)).Select(x => x.AmountStake).FirstOrDefault();
                var exposureStack = Math.Abs(unsettleBetExposure);
                exposureStack = Math.Truncate(100 * exposureStack) / 100;
                return new CommonReturnResponse
                {
                    Data = exposureStack,
                    Message = MessageStatus.Success,
                    IsSuccess = true,
                    Status = ResponseStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> UpdateAssignCoinAsync(long AssignCoin, int LoginUserId)
        {
            Users users = null;
            bool _result = false;
            try
            {
                if (LoginUserId > 0)
                {
                    users = await _baseRepository.GetDataByIdAsync<Users>(LoginUserId);
                    if (users != null)
                    {
                        users.AssignCoin = users.AssignCoin - AssignCoin;
                        _result = await _baseRepository.UpdateAsync(users) == 1;
                        if (_result) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    }
                    else
                    {
                        return new CommonReturnResponse() { IsSuccess = false, Status = ResponseStatusCode.NOTFOUND, Message = MessageStatus.NoRecord, Data = null };
                    }
                    return new CommonReturnResponse
                    {
                        Data = _result,
                        Message = _result ? MessageStatus.Update : MessageStatus.Error,
                        IsSuccess = _result,
                        Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                    };
                }
                return new CommonReturnResponse() { IsSuccess = false, Status = ResponseStatusCode.BADREQUEST, Message = MessageStatus.InvalidData, Data = null };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (users != null) { users = null; } }
        }

        public async Task<CommonReturnResponse> DepositAssignCoinAsync(long assignCoin, int parentId, int userId, int UserRoleId)
        {
            bool _result = false;
            try
            {
                var depositCoin = new AccountStatement
                {
                    CreatedDate = DateTime.Now,
                    Deposit = assignCoin,
                    Withdraw = 0,
                    Balance = assignCoin,
                    Remark = "Deposit",
                    FromUserId = parentId,
                    ToUserId = userId,
                    ToUserRoleId = UserRoleId,
                    IsAccountStatement = true
                };
                _result = await _baseRepository.InsertAsync(depositCoin) > 0;

                //string sql = string.Format(@"select top 1 *  from AccountStatement where ToUserId = {0} and IsAccountStatement = 1 order by id desc", userId);
                //var latestAccountStatement = (await _baseRepository.QueryAsync<AccountStatement>(sql)).FirstOrDefault();
                //var withdrawCoin = new AccountStatement
                //{
                //    CreatedDate = DateTime.Now,
                //    Deposit = 0,
                //    Withdraw = assignCoin,
                //    Balance = latestAccountStatement.Balance - assignCoin,
                //    Remark = "Assign Coin to other user",
                //    FromUserId = userId,
                //    ToUserId = userId,
                //    ToUserRoleId = UserRoleId
                //};

                if (_result == true) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                return new CommonReturnResponse
                {
                    Data = _result,
                    Message = _result ? MessageStatus.Update : MessageStatus.Error,
                    IsSuccess = _result,
                    Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> DepositWithdrawCoinAsync(long Amount, int parentId, int userId, int UserRoleId, string Remark, bool Type)
        {
            bool _result = false;
            try
            {
                string query = string.Format(@"select top 1 *  from AccountStatement where ToUserId = {0} order by id desc", userId);
                var balance = (await _baseRepository.QueryAsync<AccountStatement>(query)).Select(x => x.Balance).FirstOrDefault();

                var depositWithdrawCoin = new AccountStatement
                {
                    CreatedDate = DateTime.Now,
                    Deposit = Type == true ? Amount : 0,
                    Withdraw = Type == false ? Amount : 0,
                    Balance = Type == true ? balance + Amount : balance - Amount,
                    Remark = Remark,
                    FromUserId = parentId,
                    ToUserId = userId,
                    ToUserRoleId = UserRoleId,
                    IsAccountStatement = true
                };
                _result = await _baseRepository.InsertAsync(depositWithdrawCoin) > 0;
                if (_result == true) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                return new CommonReturnResponse
                {
                    Data = _result,
                    Message = _result ? MessageStatus.Save : MessageStatus.Error,
                    IsSuccess = _result,
                    Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> ExposureLimitAsync(long Limit, int userId)
        {
            bool _result = false;
            try
            {
                if (userId > 0)
                {
                    var users = await _baseRepository.GetDataByIdAsync<Users>(userId);
                    if (users != null)
                    {
                        users.ExposureLimit = Limit;
                        _result = await _baseRepository.UpdateAsync(users) == 1;
                        if (_result) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    }
                    else
                    {
                        return new CommonReturnResponse() { IsSuccess = false, Status = ResponseStatusCode.NOTFOUND, Message = MessageStatus.NoRecord, Data = null };
                    }
                    return new CommonReturnResponse
                    {
                        Data = _result,
                        Message = _result ? MessageStatus.Update : MessageStatus.Error,
                        IsSuccess = _result,
                        Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                    };
                }
                return new CommonReturnResponse() { IsSuccess = false, Status = ResponseStatusCode.BADREQUEST, Message = MessageStatus.InvalidData, Data = null };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }


        public async Task<CommonReturnResponse> ProfitLossUserAsync(long Amount, int parentId, int userId, int UserRoleId, string Remark, bool Type)
        {
            bool _result = false;
            try
            {
                string query = string.Format(@"select top 1 *  from AccountStatement where ToUserId = {0} order by id desc", userId);
                var balance = (await _baseRepository.QueryAsync<AccountStatement>(query)).Select(x => x.Balance).FirstOrDefault();

                var profitLoss = new AccountStatement
                {
                    CreatedDate = DateTime.Now,
                    Deposit = Type == true ? Amount : 0,
                    Withdraw = Type == false ? Amount : 0,
                    Balance = Type == true ? balance + Amount : balance - Amount,
                    Remark = Remark,
                    FromUserId = parentId,
                    ToUserId = userId,
                    ToUserRoleId = UserRoleId,
                    IsAccountStatement = false
                };
                _result = await _baseRepository.InsertAsync(profitLoss) > 0;
                if (_result == true) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                return new CommonReturnResponse
                {
                    Data = _result,
                    Message = _result ? MessageStatus.Save : MessageStatus.Error,
                    IsSuccess = _result,
                    Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetUserRolesAsync()
        {
            try
            {
                var userRoles = await _baseRepository.GetListAsync<UserRoles>();
                return new CommonReturnResponse
                {
                    Data = userRoles,
                    Message = userRoles.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = userRoles.Count > 0,
                    Status = userRoles.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAllUsers(int RoleId, int LoginUserId)
        {
            try
            {
                string sql = string.Format(@"select Users.Id,Users.FullName,Users.RoleId,Users.ParentId,(select top 1 Balance from AccountStatement where ToUserId = Users.Id order by Id desc) as AssignCoin from Users where RoleId > {0}", RoleId);
                var users = (await _baseRepository.QueryAsync<Users>(sql)).ToList();

                var u = users.Where(x => x.ParentId == LoginUserId).ToList();
                var totalUser = u;
                if (u != null && u.Count() > 0)
                {
                    for (; ; )
                    {
                        var ids = u.Select(x => x.Id).ToList();
                        var u1 = users.Where(x => ids.Contains(x.ParentId)).ToList();
                        if (u1.Count == 0)
                        {
                            break;
                        }

                        totalUser.AddRange(u1);
                        u = u1;
                    }
                }

                //totalUser = totalUser != null && totalUser.Count() > 0 ? totalUser.Where(x => x.RoleId == RoleId).ToList() : totalUser;
                foreach (var item in totalUser)
                {
                    var abc = await GetOpeningBalanceAsync(item.Id);
                    item.AssignCoin = Convert.ToInt64(abc.Data);
                }
                return new CommonReturnResponse
                {
                    Data = totalUser,
                    Message = totalUser.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = totalUser.Count > 0,
                    Status = totalUser.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetUserDetailAsync(int UserId)
        {
            try
            {
                var user = await _baseRepository.GetDataByIdAsync<Users>(UserId);
                return new CommonReturnResponse
                {
                    Data = user,
                    Message = user != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = user != null,
                    Status = user != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetUsersByParentIdAsync(int LoginUserId, int RoleId, int UserId)
        {
            IDictionary<string, object> _keyValues = null;
            List<UsersVM> usersVM = new List<UsersVM>();
            try
            {
                var userRoles = await _baseRepository.GetListAsync<UserRoles>();
                _keyValues = new Dictionary<string, object> { { "Id", LoginUserId } };
                var loginUser = (await _baseRepository.SelectAsync<Users>(_keyValues)).FirstOrDefault();

                string query = string.Format(@"select Users.*,(select top 1 Balance from AccountStatement where ToUserId = Users.Id order by Id desc) as AvailableBalance,(select sum(AmountStake + ResultAmount) from Bets where IsSettlement = 1 and UserId = Users.Id) as ProfitAndLoss from Users");
                var result = await _baseRepository.GetQueryMultipleAsync(query, null, gr => gr.Read<UsersVM>());
                usersVM = (result[0] as List<UsersVM>).ToList();

                //var isAbleToChange = RoleId > 0 ? loginUser.RoleId == RoleId - 1 || loginUser.RoleId == 2 || loginUser.RoleId == 3 || loginUser.RoleId == 4 : false;

                var u = usersVM.Where(x => x.ParentId == LoginUserId).ToList();
                var totalUser = u;
                var loginParentUser = usersVM.Where(x => x.Id == LoginUserId).FirstOrDefault();
                if (u != null && u.Count() > 0)
                {
                    for (; ; )
                    {
                        var ids = u.Select(x => x.Id).ToList();
                        var u1 = usersVM.Where(x => ids.Contains(x.ParentId)).ToList();
                        if (u1.Count == 0)
                        {
                            break;
                        }

                        totalUser.AddRange(u1);
                        u = u1;
                    }
                }

                var model = new RegisterListVM();
                totalUser = totalUser != null && totalUser.Count() > 0 ? totalUser.Where(x => x.RoleId == RoleId).ToList() : totalUser;
                if (UserId > 0)
                {
                    totalUser = totalUser.Where(x => x.Id == UserId).ToList();
                }
                model.Users = totalUser;
                foreach (var item in model.Users)
                {
                    item.IsAbleToChange = model.Users.Where(x => x.ParentId == LoginUserId).FirstOrDefault() != null ? true : false;
                }
                var roleName = userRoles.Where(y => y.Id == RoleId).Select(x => x.Name).FirstOrDefault();

                model = new RegisterListVM
                {
                    LoginUserId = loginUser.Id,
                    LoginUserRole = loginUser.RoleId,
                    LoginUser = loginParentUser,
                    RoleName = roleName,
                    Users = model.Users,
                    UserRoles = userRoles
                };

                return new CommonReturnResponse
                {
                    Data = model,
                    Message = model != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = model != null,
                    Status = model != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> CheckCoinAsync(int ParentUserId, long AssignCoin)
        {
            IDictionary<string, object> _keyValues = null;
            try
            {
                _keyValues = new Dictionary<string, object> { { "ParentId", ParentUserId } };
                var usersList = (await _baseRepository.SelectAsync<Users>(_keyValues)).OrderByDescending(a => a.Id).ToList();
                return new CommonReturnResponse
                {
                    Data = usersList,
                    Message = usersList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = usersList.Count > 0,
                    Status = usersList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> UpdateUserDetailAsync(string query)
        {
            Users users = null;
            bool _result = false;
            try
            {
                await _baseRepository.QueryAsync<Users>(query);
                _baseRepository.Commit();
                return new CommonReturnResponse
                {
                    Data = null,
                    Message = MessageStatus.Update,
                    IsSuccess = true,
                    Status = ResponseStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (users != null) { users = null; } }
        }

        public async Task<CommonReturnResponse> UpdateUserStatusAsync(int Status, int UserId)
        {
            bool _result = false;
            try
            {
                var user = await _baseRepository.GetDataByIdAsync<Users>(UserId);
                if (user != null)
                {
                    user.Status = Status;
                    _result = await _baseRepository.UpdateAsync(user) == 1;
                    if (_result) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                }
                return new CommonReturnResponse
                {
                    Data = null,
                    Message = MessageStatus.Update,
                    IsSuccess = true,
                    Status = ResponseStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> UserLoginStatusAsync(UserStatus model)
        {
            try
            {
                var userStatus = await _baseRepository.GetDataByIdAsync<UserStatus>(model.Id);
                if (userStatus != null)
                {
                    userStatus.Status = model.Status;
                    userStatus.LogoutTime = DateTime.Now;
                    bool _result = await _baseRepository.UpdateAsync(userStatus) == 1;
                    if (_result) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse
                    {
                        Data = model.Id,
                        Message = MessageStatus.Update,
                        IsSuccess = true,
                        Status = ResponseStatusCode.OK
                    };
                }
                else
                {
                    model.LoginTime = DateTime.Now;
                    var reusltId = await _baseRepository.InsertAsync(model);
                    if (reusltId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse
                    {
                        Data = reusltId,
                        Message = MessageStatus.Create,
                        IsSuccess = true,
                        Status = ResponseStatusCode.OK
                    };
                }
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> AddOrUpdateRollingCommissionAsync(RollingCommision model)
        {
            bool _result = false;
            try
            {
                if (model.Id > 0)
                {
                    _result = await _baseRepository.UpdateAsync(model) == 1;
                    if (_result == true) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse
                    {
                        Data = _result,
                        Message = _result ? MessageStatus.Update : MessageStatus.Error,
                        IsSuccess = _result,
                        Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                    };
                }
                else
                {
                    _result = await _baseRepository.InsertAsync(model) > 0;
                    if (_result == true) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse
                    {
                        Data = _result,
                        Message = _result ? MessageStatus.Create : MessageStatus.Error,
                        IsSuccess = _result,
                        Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                    };
                }
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetCreditReferenceAsync(int UserId)
        {
            List<CreditReferenceVM> creditReferenceVM = null;
            try
            {
                var userList = await _baseRepository.GetListAsync<Users>();

                string query = string.Format(@"select top 1 Balance as NewBalance,(SELECT Balance FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS rownumber, Balance FROM AccountStatement where ToUserId = {0} and IsAccountStatement = 1) AS foo WHERE rownumber = 2) as OldBalance,FromUserId,ToUserId from AccountStatement where ToUserId = {0} and IsAccountStatement = 1 order by id desc", UserId);
                var result = await _baseRepository.GetQueryMultipleAsync(query, null, gr => gr.Read<CreditReferenceVM>());
                creditReferenceVM = (result[0] as List<CreditReferenceVM>).ToList();
                foreach (var item in creditReferenceVM)
                {
                    item.FromName = userList.Where(x => x.Id == item.FromUserId).Select(y => y.FullName).FirstOrDefault();
                    item.ToName = userList.Where(x => x.Id == item.ToUserId).Select(y => y.FullName).FirstOrDefault();
                }
                return new CommonReturnResponse
                {
                    Data = creditReferenceVM,
                    Message = MessageStatus.Success,
                    IsSuccess = true,
                    Status = ResponseStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<double> GetBackAndLayOpeningAndExposureAmountAsync(int UserId, string marketId, int SportId)
        {
            string[] arr = new string[0];
            string sql = string.Empty;
            string _condition = string.Empty;
            UserBetPagination userBetPagination = new UserBetPagination();
            var teamSelectionIds = new List<TeamSelectionId>();
            var teamAmount = new List<TeamAmount>();
            var teamNameAmount = new List<TeamNameAmount>();
            var teamAmountFinal = new List<TeamAmount>();
            var teamNameAmountFinal = new List<TeamNameAmount>();
            string oddsStr = "", responseStr = "";
            double amount = 0;
            try
            {
                var teamNameResponse = await _requestServices.GetAsync<TeamNameResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                var runnerNames = teamNameResponse.data.Where(x => x.marketId == marketId).FirstOrDefault();

                teamSelectionIds = commonFun.GetTeamName(runnerNames);

                if (UserId > 0)
                {
                    sql = $"select * from Bets where IsSettlement = 2 and MarketId='{marketId}' and userid = {UserId}";
                }
                else
                {
                    sql = $"select * from Bets where IsSettlement = 2 and MarketId='{marketId}'";
                }

                var betList = (await _baseRepository.QueryAsync<Bets>(sql)).ToList();
                if (betList.Count <= 0)
                {
                    if (teamSelectionIds.Count > 0)
                    {
                        for (int i = 0; i < teamSelectionIds.Count; i++)
                        {
                            if (UserId > 0)
                            {
                                teamAmount.Add(new TeamAmount
                                {
                                    selectionId = Convert.ToInt64(teamSelectionIds[i].selectionId),
                                    amount = 0
                                });
                            }
                            else
                            {
                                teamNameAmount.Add(new TeamNameAmount
                                {
                                    selectionName = teamSelectionIds[i].teamName,
                                    amount = 0
                                });
                            }
                        }
                    }
                    if (UserId > 0)
                    {
                        teamAmountStr = JsonConvert.SerializeObject(teamAmount);
                        teamAmountStr = teamAmountStr.Replace("\"selectionId\":", "\"");
                        teamAmountStr = teamAmountStr.Replace(",\"", "\"");
                        teamAmountStr = teamAmountStr.Replace("amount\"", "");
                        teamAmountStr = teamAmountStr.Replace("amount\"", "");
                    }
                    else
                    {
                        teamAmountStr = JsonConvert.SerializeObject(teamNameAmount);
                        teamAmountStr = teamAmountStr.Replace("\"selectionName\":", "\"");
                        teamAmountStr = teamAmountStr.Replace(",\"", "\"");
                        teamAmountStr = teamAmountStr.Replace("amount\"", "");
                        teamAmountStr = teamAmountStr.Replace("amount\"", "");
                    }
                    return 0;
                }

                if (teamSelectionIds.Count > 0)
                {
                    for (int i = 0; i < betList.Count; i++)
                    {
                        if (betList[i].Type == "back")
                        {
                            for (int j = 0; j < teamSelectionIds.Count; j++)
                            {
                                if (UserId > 0)
                                {
                                    if (betList[i].SelectionId == teamSelectionIds[j].selectionId)
                                    {
                                        oddsStr = oddsStr + teamSelectionIds[j].selectionId + ":" + ((betList[i].AmountStake * betList[i].OddsRequest) - betList[i].AmountStake).ToString() + "| ";
                                    }
                                    else
                                    {
                                        oddsStr = oddsStr + teamSelectionIds[j].selectionId + ":" + (-betList[i].AmountStake).ToString() + "| ";
                                    }
                                }
                                else
                                {
                                    if (betList[i].SelectionId == teamSelectionIds[j].selectionId)
                                    {
                                        oddsStr = oddsStr + teamSelectionIds[j].teamName + ":" + ((betList[i].AmountStake * betList[i].OddsRequest) - betList[i].AmountStake).ToString() + "| ";
                                    }
                                    else
                                    {
                                        oddsStr = oddsStr + teamSelectionIds[j].teamName + ":" + (-betList[i].AmountStake).ToString() + "| ";
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < teamSelectionIds.Count; j++)
                            {
                                if (UserId > 0)
                                {
                                    if (betList[i].SelectionId == teamSelectionIds[j].selectionId)
                                    {
                                        oddsStr = oddsStr + teamSelectionIds[j].selectionId + ":" + (-((betList[i].AmountStake * betList[i].OddsRequest) - betList[i].AmountStake)).ToString() + "| ";
                                    }
                                    else
                                    {
                                        oddsStr = oddsStr + teamSelectionIds[j].selectionId + ":" + (betList[i].AmountStake).ToString() + "| ";
                                    }
                                }
                                else
                                {
                                    if (betList[i].SelectionId == teamSelectionIds[j].selectionId)
                                    {
                                        oddsStr = oddsStr + teamSelectionIds[j].teamName + ":" + (-((betList[i].AmountStake * betList[i].OddsRequest) - betList[i].AmountStake)).ToString() + "| ";
                                    }
                                    else
                                    {
                                        oddsStr = oddsStr + teamSelectionIds[j].teamName + ":" + (betList[i].AmountStake).ToString() + "| ";
                                    }
                                }
                            }
                        }

                        oddsStr = oddsStr.Substring(0, oddsStr.Length - 2);
                        responseStr = responseStr + oddsStr + "@";
                        oddsStr = "";
                    }

                    responseStr = responseStr.Substring(0, responseStr.Length - 1);
                    var responseArr = responseStr.Split('@');
                    arr = new string[responseArr[0].Split('|').Count()];

                    for (int k = 0; k < responseArr.Length; k++)
                    {
                        var z = responseArr[k].Split('|');
                        for (int q = 0; q < z.Length; q++)
                        {
                            if (UserId > 0)
                            {
                                teamAmount.Add(new TeamAmount
                                {
                                    selectionId = Convert.ToInt64(z[q].Split(':')[0]),
                                    amount = Math.Truncate(100 * Convert.ToDouble(z[q].Split(':')[1])) / 100
                                });
                            }
                            else
                            {
                                teamNameAmount.Add(new TeamNameAmount
                                {
                                    selectionName = z[q].Split(':')[0],
                                    amount = Math.Truncate(100 * Convert.ToDouble(z[q].Split(':')[1])) / 100
                                });
                            }
                        }
                    }
                }

                for (int kk = 0; kk < teamSelectionIds.Count; kk++)
                {
                    teamAmountFinal.Add(new TeamAmount
                    {
                        selectionId = teamSelectionIds[kk].selectionId,
                        amount = teamAmount.Where(x => x.selectionId == teamSelectionIds[kk].selectionId).Sum(y => y.amount)
                    });
                }

                if(responseStr.Length > 0)
                    responseStr = responseStr.Substring(0, responseStr.Length - 1);
                //if (UserId > 0)
                //{
                //    teamAmountStr = JsonConvert.SerializeObject(teamAmountFinal);
                //    teamAmountStr = teamAmountStr.Replace("\"selectionId\":", "\"");
                //    teamAmountStr = teamAmountStr.Replace(",\"", "\"");
                //    teamAmountStr = teamAmountStr.Replace("amount\"", "");
                //}
                //else
                //{
                //    teamAmountStr = JsonConvert.SerializeObject(teamAmountFinal);
                //    teamAmountStr = teamAmountStr.Replace("\"selectionName\":", "\"");
                //    teamAmountStr = teamAmountStr.Replace(",\"", "\"");
                //    teamAmountStr = teamAmountStr.Replace("amount\"", "");
                //}
                double returnAmt = 0;
                for (int iii = 0; iii < teamAmountFinal.Count; iii++)
                {
                    if (teamAmountFinal[iii].amount < 0)
                    {
                        if (returnAmt < teamAmountFinal[iii].amount)
                            amount = teamAmountFinal[iii].amount;
                        else
                            returnAmt = teamAmountFinal[iii].amount;
                        amount = returnAmt;
                    }
                }

                return amount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
