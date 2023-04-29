using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Veelki.Core.IServices;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Entities;
using Veelki.Data.Repository;
using RB444.Model.ViewModel;
using Veelki.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veelki.Model.ViewModel;

namespace Veelki.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository _baseRepository;
        private readonly ICookieService _cookieService;
        CommonFun commonFun = new CommonFun();

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, IRequestServices requestServices, IConfiguration configuration, IBaseRepository baseRepository, ICookieService cookieService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _requestServices = requestServices;
            _configuration = configuration;
            _baseRepository = baseRepository;
            _cookieService = cookieService;
        }

        public ActionResult Login(string returnUrl = null)
        {
            ViewBag.ValidationCode = commonFun.GenerateRandomNo();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Veelki.Models.ViewModel.LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ValidationCode = commonFun.GenerateRandomNo();
                return View(model);
            }
            try
            {
                if (model.ValidationCode != model.TxtValidationCode)
                {
                    ViewBag.ValidationCode = commonFun.GenerateRandomNo();
                    return View(model);
                }
                Users user = null;
                if (model.UserName.IndexOf("@") > 0)
                {
                    user = await _userManager.FindByEmailAsync(model.UserName);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(model.UserName);
                }
                if (user.RoleId == 7)
                {
                    return View(model);
                }
                if (user != null && user.Status == 1)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _cookieService.Set("loginUserDetail", JsonConvert.SerializeObject(user), 0);

                        string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                        if (ipAddress != "::1" && user.RoleId != 1)
                        {
                            var locationModel = commonFun.GetIpInfo(ipAddress);
                            try
                            {
                                var activityLog = new ActivityLog
                                {
                                    Address = $"{locationModel.city}/{locationModel.regionName}/{locationModel.country}/{locationModel.zip}",
                                    IpAddress = locationModel.query,
                                    ISP = locationModel.isp,
                                    LoginDate = DateTime.Now,
                                    UserId = user.Id,
                                    Status = "login_success"
                                };

                                var _result = await _baseRepository.InsertAsync(activityLog);
                                if (_result > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                            }
                            catch (Exception ex)
                            {
                                var activityLog = new ActivityLog
                                {
                                    Address = $"{locationModel.city}/{locationModel.regionName}/{locationModel.country}/{locationModel.zip}",
                                    IpAddress = locationModel.query,
                                    ISP = locationModel.isp,
                                    LoginDate = DateTime.Now,
                                    UserId = user.Id,
                                    Status = "login_fail"
                                };
                                var _result = await _baseRepository.InsertAsync(activityLog);
                                if (_result > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                            }
                        }

                        return Redirect("/Home/Index");
                    }
                }
                TempData["ErrorMsg"] = "User name and password not matched";
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(Veelki.Models.Model.RegisterViewModel model)
        {
            var contextUser = HttpContext.User;
            CommonReturnResponse commonModel = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var loginUser = await _userManager.FindByNameAsync(contextUser.Identity.Name);

                    if (loginUser.AssignCoin < model.AssignCoin && loginUser.RoleId != 1 && loginUser.RoleId != 2)
                    {
                        var message = loginUser.AssignCoin == 0 ? "no coin availble" : $"only {loginUser.AssignCoin}";
                        commonModel = new CommonReturnResponse { Data = null, Message = $"You have {message} coins remaining.", IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };
                        return Json(JsonConvert.SerializeObject(commonModel));
                    }

                    var user = new Users
                    {
                        UserName = model.UserName,
                        FullName = model.FullName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        RoleId = (loginUser.RoleId + 1),
                        CreatedDate = DateTime.Now,
                        RollingCommission = model.RollingCommission,
                        AssignCoin = model.AssignCoin,
                        Commision = model.Commision,
                        ExposureLimit = model.ExposureLimit,
                        ParentId = loginUser.Id,
                        Status = 1,
                        City = model.City,
                        State = model.State
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (user.RoleId != 1 && user.RoleId != 2)
                        {
                            if (loginUser.RoleId != 1 && loginUser.RoleId != 2)
                            {
                                await _requestServices.GetAsync<CommonReturnResponse>(string.Format("{0}Account/UpdateAssignCoin?AssignCoin={1}&LoginUserId={2}", _configuration["ApiKeyUrl"], model.AssignCoin, user.ParentId));
                            }

                            await _requestServices.GetAsync<CommonReturnResponse>(string.Format("{0}Account/DepositAssignCoin?AssignCoin={1}&ParentId={2}&UserId={3}&UserRoleId={4}", _configuration["ApiKeyUrl"], model.AssignCoin, user.ParentId, user.Id, model.RoleId));

                            var rollingCommision = new RollingCommision();
                            if (model.RollingCommission)
                            {
                                rollingCommision = new RollingCommision
                                {
                                    FromUserId = user.ParentId,
                                    ToUserId = user.Id,
                                    Fancy = model.RollingCommisionVm.Fancy,
                                    Casino = model.RollingCommisionVm.Casino,
                                    Bookmaker = model.RollingCommisionVm.Bookmaker,
                                    Binary = model.RollingCommisionVm.Binary,
                                    Matka = model.RollingCommisionVm.Matka,
                                    SportBook = model.RollingCommisionVm.SportBook,
                                };
                            }
                            else
                            {
                                rollingCommision = new RollingCommision
                                {
                                    FromUserId = user.ParentId,
                                    ToUserId = user.Id,
                                    Fancy = 0,
                                    Casino = 0,
                                    Bookmaker = 0,
                                    Binary = 0,
                                    Matka = 0,
                                    SportBook = 0,
                                };
                            }
                            commonModel = await _requestServices.PostAsync<RollingCommision, CommonReturnResponse>(String.Format("{0}Account/SaveRollingCommission", _configuration["ApiKeyUrl"]), rollingCommision);
                            if (commonModel.IsSuccess)
                            {
                                return Json(JsonConvert.SerializeObject(commonModel));
                            }
                        }
                        //var data = JsonConvert.SerializeObject(commonModel);
                        commonModel = new CommonReturnResponse { Data = null, Message = MessageStatus.Success, IsSuccess = true, Status = ResponseStatusCode.OK };
                        return Json(JsonConvert.SerializeObject(commonModel));
                    }
                    commonModel = new CommonReturnResponse { Data = null, Message = result.ToString(), IsSuccess = false, Status = ResponseStatusCode.BADREQUEST }; ;
                    return Json(JsonConvert.SerializeObject(commonModel));
                }

                commonModel = new CommonReturnResponse { Data = null, Message = CustomMessageStatus.InvalidModelState, IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };
                return Json(JsonConvert.SerializeObject(commonModel));
            }
            catch (Exception ex)
            {
                var errorArr = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                var errorHtml = "<ul>";
                if (errorArr != null && errorArr.Count > 0)
                {
                    foreach (var error in errorArr)
                    {
                        errorHtml += "<li>" + error + "</li>";
                    }
                }

                errorHtml += "</ul>";
                commonModel = new CommonReturnResponse { Data = false, Message = errorHtml, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
                return Json(JsonConvert.SerializeObject(commonModel));
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ResetPasswordViewModel model)
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            var data = new CommonReturnResponse();

            if (model.OldPassword.Length > 0)
            {
                var isOldPassword = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                if (isOldPassword)
                {
                    var Code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, Code, model.Password);
                    if (result.Succeeded)
                    {
                        data = new CommonReturnResponse()
                        {
                            Data = result,
                            IsSuccess = true,
                            Message = CustomMessageStatus.resetPwd,
                            Status = ResponseStatusCode.OK
                        };
                        return Json(JsonConvert.SerializeObject(data));
                    }

                    data = new CommonReturnResponse()
                    {
                        Data = result.Errors,
                        IsSuccess = false,
                        Message = result.ToString(),
                        Status = ResponseStatusCode.NOTACCEPTABLE
                    };
                    return Json(JsonConvert.SerializeObject(data));
                }
                else
                {
                    data = new CommonReturnResponse()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = CustomMessageStatus.oldPwd,
                        Status = ResponseStatusCode.NOTACCEPTABLE
                    };
                    return Json(JsonConvert.SerializeObject(data));
                }
            }
            else
            {
                var profileUser = await _userManager.FindByIdAsync(model.UserId);
                var Code = await _userManager.GeneratePasswordResetTokenAsync(profileUser);
                var result = await _userManager.ResetPasswordAsync(profileUser, Code, model.Password);
                if (result.Succeeded)
                {
                    data = new CommonReturnResponse()
                    {
                        Data = result,
                        IsSuccess = true,
                        Message = CustomMessageStatus.resetPwd,
                        Status = ResponseStatusCode.OK
                    };
                    return Json(JsonConvert.SerializeObject(data));
                }

                data = new CommonReturnResponse()
                {
                    Data = result.Errors,
                    IsSuccess = false,
                    Message = result.ToString(),
                    Status = ResponseStatusCode.NOTACCEPTABLE
                };
                return Json(JsonConvert.SerializeObject(data));
            }

            data = new CommonReturnResponse()
            {
                IsSuccess = false,
                Message = "Something Went Wrong."
            };
            return Json(JsonConvert.SerializeObject(data));

            //CommonReturnResponse commonModel = null;
            //try
            //{
            //    commonModel = await _rqs.PostAsync<ResetPasswordViewModel, CommonReturnResponse>(String.Format("{0}System/Account/ResetPassword", _configuration["ApiUrl"]), model);
            //    var data = JsonConvert.SerializeObject(commonModel);
            //    return Json(data);

            //}
            //catch (Exception ex)
            //{
            //    var data = new CommonReturnResponse()
            //    {
            //        IsSuccess = false,
            //        Message = ex.Message
            //    };
            //    return Json(JsonConvert.SerializeObject(data));
            //}
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            _cookieService.Remove("loginUserDetail");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<ActionResult> ActivityLog()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }

            CommonReturnResponse commonModel = null;
            List<ActivityLogVM> activityLogVM = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetActivityLog", _configuration["ApiKeyUrl"]));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    activityLogVM = jsonParser.ParsJson<List<ActivityLogVM>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(activityLogVM);
        }

        [HttpGet]
        public async Task<ActionResult> AccountStatement()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }

            CommonReturnResponse commonModel = null;
            List<AccountStatementVM> accountStatementVM = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatementForSuperAdmin?AdminId={1}", _configuration["ApiKeyUrl"], user.Id));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(accountStatementVM);
        }

        [HttpGet]
        public async Task<ActionResult> AccountSummary()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }

            //CommonReturnResponse commonModel = null;
            //List<AccountStatementVM> accountStatementVM = null;
            //try
            //{
            //    commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatementForSuperAdmin?AdminId={1}", _configuration["ApiKeyUrl"], user.Id));
            //    if (commonModel.IsSuccess && commonModel.Data != null)
            //    {
            //        accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BettingHistory()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }

            //CommonReturnResponse commonModel = null;
            //List<AccountStatementVM> accountStatementVM = null;
            //try
            //{
            //    commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatementForSuperAdmin?AdminId={1}", _configuration["ApiKeyUrl"], user.Id));
            //    if (commonModel.IsSuccess && commonModel.Data != null)
            //    {
            //        accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BettingProfile()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }

            //CommonReturnResponse commonModel = null;
            //List<AccountStatementVM> accountStatementVM = null;
            //try
            //{
            //    commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatementForSuperAdmin?AdminId={1}", _configuration["ApiKeyUrl"], user.Id));
            //    if (commonModel.IsSuccess && commonModel.Data != null)
            //    {
            //        accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> MyAccount()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }

            ViewBag.HeaderItem = CommonFun.HeaderItem.MyAccount;
            //CommonReturnResponse commonModel = null;
            //List<AccountStatementVM> accountStatementVM = null;
            //try
            //{
            //    commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatementForSuperAdmin?AdminId={1}", _configuration["ApiKeyUrl"], user.Id));
            //    if (commonModel.IsSuccess && commonModel.Data != null)
            //    {
            //        accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Banking()
        {
            var user = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null; if (user != null) { ViewBag.LoginUser = user; } else { return RedirectToAction("Login", "Account"); }
            CommonReturnResponse commonModel = null;
            var model = new RegisterListVM();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetUsersByParentId?ParentId={1}&RoleId={2}&UserId={3}", _configuration["ApiKeyUrl"], user.ParentId, (user.RoleId + 1), 0));
                model = jsonParser.ParsJson<RegisterListVM>(Convert.ToString(commonModel.Data));
                ViewBag.roleIdBass = 3;
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
            //CommonReturnResponse commonModel = null;
            //List<AccountStatementVM> accountStatementVM = null;
            //try
            //{
            //    commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatementForSuperAdmin?AdminId={1}", _configuration["ApiKeyUrl"], user.Id));
            //    if (commonModel.IsSuccess && commonModel.Data != null)
            //    {
            //        accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> DepositWithdrawCoin(DepositWithdrawVM[] oDepositWithdrawVM)
        {
            CommonReturnResponse commonModel = null;
            bool allBalanceZero = false;
            try
            {
                var loginUser = Request.Cookies["loginUserDetail"] != null ? JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]) : null;
                foreach (var item in oDepositWithdrawVM)
                {
                    if (item.Balance > 0)
                    {
                        allBalanceZero = true;
                        commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/DepositWithdrawCoin?Amount={1}&ParentId={2}&UserId={3}&UserRoleId={4}&&Remark={5}&Type={6}&Password={7}", _configuration["ApiKeyUrl"], item.Balance, loginUser.Id, item.UserId, loginUser.RoleId + 1, item.Remark, item.IsDeposit, item.Password));
                    }
                }

                return Json(commonModel);
            }
            catch (Exception ex)
            {
                commonModel = new CommonReturnResponse()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message,
                    Status = ResponseStatusCode.NOTACCEPTABLE
                };
                return Json(JsonConvert.SerializeObject(commonModel));
            }
        }
    }
}
