using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Veelki.Core.IServices;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Entities;
using Veelki.Data.Repository;
using Veelki.Models.Model;
using System;
using System.Threading.Tasks;

namespace Veelki.Api.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IAccountService _accountService;
        private readonly IBaseRepository _baseRepository;
        CommonFun commonFun = new CommonFun();
        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, IAccountService accountService, IBaseRepository baseRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _baseRepository = baseRepository;
        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<CommonReturnResponse> Login(LoginViewModel model, string returnUrl = null)
        {
            bool isEmailOrUsername = false;
            if (!ModelState.IsValid)
            {
                return new CommonReturnResponse { Data = null, Message = CustomMessageStatus.InvalidModelState, IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };
            }
            try
            {
                Users user = null;
                if (model.email.IndexOf("@") > 0)
                {
                    user = await _userManager.FindByEmailAsync(model.email);
                    isEmailOrUsername = false;
                }
                else
                {
                    user = await _userManager.FindByNameAsync(model.email);
                    isEmailOrUsername = true;
                    if (user == null)
                    {
                        user = await _userManager.FindByEmailAsync(model.email);
                        isEmailOrUsername = false;
                    }
                }
                if (user != null)
                {
                    if (user.Status == 2)
                    {
                        return new CommonReturnResponse { Data = null, Message = "User suspended.", IsSuccess = false, Status = ResponseStatusCode.NOTACCEPTABLE };
                    }
                    else if (user.Status == 3)
                    {
                        return new CommonReturnResponse { Data = null, Message = "User blocked.", IsSuccess = false, Status = ResponseStatusCode.NOTACCEPTABLE };
                    }
                    else if (user.Status == 1)
                    {
                        Microsoft.AspNetCore.Identity.SignInResult result = null;
                        if (isEmailOrUsername == false)
                        {
                            result = await _signInManager.PasswordSignInAsync(model.email, model.password, model.rememberme, lockoutOnFailure: false);
                        }
                        else
                        {
                            model.UserName = model.email;
                            result = await _signInManager.PasswordSignInAsync(model.UserName, model.password, model.rememberme, lockoutOnFailure: false);
                        }
                        if (result.Succeeded)
                        {
                            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                            if (ipAddress != "::1")
                            {
                                var locationModel = commonFun.GetIpInfo(ipAddress);
                                var activityLog = new ActivityLog
                                {
                                    Address = $"{locationModel.city}/{locationModel.regionName}/{locationModel.country}/{locationModel.zip}",
                                    IpAddress = locationModel.query,
                                    ISP = locationModel.isp,
                                    LoginDate = DateTime.Now,
                                    UserId = user.Id
                                };

                                var _result = await _baseRepository.InsertAsync(activityLog);
                                if (_result > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                            }

                            var response = await GetOpeningBalance(user.Id);

                            var userStatus = new UserStatus
                            {
                                Id = 0,
                                Status = true,
                                UserId = user.Id
                            };
                            var commonReturnResponse = await _accountService.UserLoginStatusAsync(userStatus);
                            int loginUserId = Convert.ToInt32(commonReturnResponse.Data);
                            var userVM = new UserModel
                            {
                                Id = user.Id,
                                UserLoginId = loginUserId,
                                UserName = user.UserName,
                                FullName = user.FullName,
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                                RoleId = user.RoleId,
                                CreatedDate = user.CreatedDate,
                                RollingCommission = user.RollingCommission,
                                AssignCoin = Convert.ToInt64(response.Data),
                                Commision = user.Commision,
                                ExposureLimit = user.ExposureLimit,
                                ParentId = user.ParentId,
                                Status = user.Status
                            };
                            return new CommonReturnResponse { Data = userVM, Message = CustomMessageStatus.Loginsuccess, IsSuccess = true, Status = ResponseStatusCode.OK };
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return new CommonReturnResponse { Data = null, Message = CustomMessageStatus.InvliadLogin, IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };
                        }
                    }
                }

                return new CommonReturnResponse { Data = null, Message = "Login name or password is invalid! Please try again.", IsSuccess = false, Status = ResponseStatusCode.NOTFOUND };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountController : Login()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("ResetPassword")]
        public async Task<CommonReturnResponse> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
                return new CommonReturnResponse { Data = model, Message = CustomMessageStatus.InvalidModelState, IsSuccess = false, Status = ResponseStatusCode.NOTACCEPTABLE };
            }

            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId.ToString());
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    // return RedirectToAction(nameof(ResetPasswordConfirmation));
                    return new CommonReturnResponse { Data = user, Message = CustomMessageStatus.userNotFound, IsSuccess = false, Status = ResponseStatusCode.NOTFOUND };
                }
                var isOldPassword = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                if (isOldPassword)
                {
                    var Code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, Code, model.Password);
                    if (result.Succeeded)
                    {
                        //return RedirectToAction(nameof(ResetPasswordConfirmation));
                        return new CommonReturnResponse { Data = result, Message = CustomMessageStatus.resetPwd, IsSuccess = true, Status = ResponseStatusCode.OK };
                    }
                    return new CommonReturnResponse { Data = result.Errors, Message = result.ToString(), IsSuccess = false, Status = ResponseStatusCode.NOTACCEPTABLE };
                }
                else
                {
                    return new CommonReturnResponse { Data = null, Message = CustomMessageStatus.oldPwd, IsSuccess = false, Status = ResponseStatusCode.NOTACCEPTABLE };
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountController : ResetPassword()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        [HttpGet, Route("Logout")]
        public async Task<CommonReturnResponse> Logout(int UserId, int UserLoginId)
        {
            try
            {
                var userStatus = new UserStatus
                {
                    Id = UserLoginId,
                    UserId = UserId,
                    Status = false
                };
                var commonReturnResponse = await _accountService.UserLoginStatusAsync(userStatus);
                var loginUserId = Convert.ToInt32(commonReturnResponse.Data);
                if (loginUserId > 0)
                {
                    await _signInManager.SignOutAsync();
                    return new CommonReturnResponse
                    {
                        Data = null,
                        Message = CustomMessageStatus.logout,
                        IsSuccess = true,
                        Status = ResponseStatusCode.OK
                    };
                }

                return new CommonReturnResponse
                {
                    Data = null,
                    Message = commonReturnResponse.Message,
                    IsSuccess = false,
                    Status = ResponseStatusCode.EXCEPTION
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountController : Logout()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        [HttpGet, Route("GetOpeningBalance")]
        public async Task<CommonReturnResponse> GetOpeningBalance(int UserId)
        {
            return await _accountService.GetOpeningBalanceAsync(UserId);
        }

        [HttpGet, Route("GetBetExposureStack")]
        public async Task<CommonReturnResponse> GetBetExposureStack(int UserId)
        {
            return await _accountService.GetBetExposureStackAsync(UserId);
        }

        [HttpGet, Route("UpdateAssignCoin")]
        public async Task<CommonReturnResponse> UpdateAssignCoin(long AssignCoin, int LoginUserId)
        {
            return await _accountService.UpdateAssignCoinAsync(AssignCoin, LoginUserId);
        }

        [HttpGet, Route("DepositAssignCoin")]
        public async Task<CommonReturnResponse> DepositAssignCoin(long AssignCoin, int ParentId, int UserId, int UserRoleId)
        {
            return await _accountService.DepositAssignCoinAsync(AssignCoin, ParentId, UserId, UserRoleId);
        }

        [HttpGet, Route("DepositWithdrawCoin")]
        public async Task<CommonReturnResponse> DepositWithdrawCoin(long Amount, int ParentId, int UserId, int UserRoleId, string Remark, bool Type, string Password)
        {
            var user = await _userManager.FindByIdAsync(ParentId.ToString());
            var checkPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!checkPassword)
            {
                return new CommonReturnResponse
                {
                    Data = null,
                    Message = "Password not match.",
                    IsSuccess = false,
                    Status = ResponseStatusCode.NOTFOUND
                };
            }
            return await _accountService.DepositWithdrawCoinAsync(Amount, ParentId, UserId, UserRoleId, Remark, Type);
        }

        [HttpGet, Route("PorfitLossUser")]
        public async Task<CommonReturnResponse> PorfitLossUser(long Amount, int ParentId, int UserId, int UserRoleId, string Remark, bool Type, string Password)
        {
            var user = await _userManager.FindByIdAsync(ParentId.ToString());
            var checkPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!checkPassword)
            {
                return new CommonReturnResponse
                {
                    Data = null,
                    Message = "Password not match.",
                    IsSuccess = false,
                    Status = ResponseStatusCode.NOTFOUND
                };
            }
            return await _accountService.ProfitLossUserAsync(Amount, ParentId, UserId, UserRoleId, Remark, Type);
        }

        [HttpGet, Route("GetUserRoles")]
        public async Task<CommonReturnResponse> GetUserRoles()
        {
            return await _accountService.GetUserRolesAsync();
        }

        [HttpGet, Route("GetAllUsers")]
        public async Task<CommonReturnResponse> GetAllUsers(int RoleId, int LoginUserId)
        {
            return await _accountService.GetAllUsers(RoleId, LoginUserId);
        }

        [HttpGet, Route("GetUsersByParentId")]
        public async Task<CommonReturnResponse> GetUsersByParentId(int ParentId, int RoleId, int UserId, int userStatus)
        {
            return await _accountService.GetUsersByParentIdAsync(ParentId, RoleId, UserId, userStatus);
        }

        [HttpGet, Route("GetUserDetail")]
        public async Task<CommonReturnResponse> GetUserDetail(int UserId)
        {
            return await _accountService.GetUserDetailAsync(UserId);
        }

        [HttpGet, Route("UpdateUserDetail")]
        public async Task<CommonReturnResponse> UpdateUserDetail(string query)
        {
            return await _accountService.UpdateUserDetailAsync(query);
        }

        [HttpGet, Route("UpdateUserStatus")]
        public async Task<CommonReturnResponse> UpdateUserStatus(int Status, int UserId)
        {
            return await _accountService.UpdateUserStatusAsync(Status, UserId);
        }

        [HttpPost, Route("UserLoginStatus")]
        public async Task<CommonReturnResponse> UserLoginStatus(UserStatus userStatus)
        {
            return await _accountService.UserLoginStatusAsync(userStatus);
        }
        [HttpGet, Route("ExposureLimit")]
        public async Task<CommonReturnResponse> ExposureLimit(long Amount, int UserId, string Password, int ParentId)
        {
            var user = await _userManager.FindByIdAsync(ParentId.ToString());
            var checkPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!checkPassword)
            {
                return new CommonReturnResponse
                {
                    Data = null,
                    Message = "Password not match.",
                    IsSuccess = false,
                    Status = ResponseStatusCode.NOTFOUND
                };
            }
            return await _accountService.ExposureLimitAsync(Amount, UserId);
        }
        [HttpPost, Route("SaveRollingCommission")]
        public async Task<CommonReturnResponse> SaveRollingCommission(RollingCommision model)
        {
            return await _accountService.AddOrUpdateRollingCommissionAsync(model);
        }

        [HttpGet, Route("GetCreditReference")]
        public async Task<CommonReturnResponse> GetCreditReference(int UserId)
        {
            return await _accountService.GetCreditReferenceAsync(UserId);
        }
    }
}
