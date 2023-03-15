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

namespace Veelki.Core.Services
{
    public class SettingService : ISettingService
    {
        private readonly IBaseRepository _baseRepository;
        public SettingService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CommonReturnResponse> AddOrUpdateSportsSettingAsync(Sports sportsSetting)
        {
            try
            {
                var sports = await _baseRepository.GetDataByIdAsync<Sports>(sportsSetting.Id);
                if (sports != null)
                {
                    int _resultId = await _baseRepository.UpdateAsync(sportsSetting);
                    if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = true, Message = MessageStatus.Update, IsSuccess = true, Status = ResponseStatusCode.OK };
                }
                else
                {
                    var _resultId = await _baseRepository.InsertAsync(sportsSetting);
                    if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = true, Message = MessageStatus.Save, IsSuccess = true, Status = ResponseStatusCode.OK };
                }
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> UpdateSeriesSettingAsync(Series seriesSetting)
        {
            try
            {
                var series = await _baseRepository.GetDataByIdAsync<Series>(seriesSetting.tournamentId);
                if (series != null)
                {
                    series.Status = seriesSetting.Status;
                    int _resultId = await _baseRepository.UpdateAsync(series);
                    if (_resultId > 0)
                    {
                        string sql = string.Format(@"Update SportsEvent set DisableMatch = {0} where tournamentId = {1}", seriesSetting.Status == false ? 1 : 0, seriesSetting.tournamentId);
                        var abc = await _baseRepository.QueryAsync<Series>(sql);
                    }

                    if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = true, Message = MessageStatus.Update, IsSuccess = true, Status = ResponseStatusCode.OK };
                }
                else
                {
                    string sql = string.Format(@"insert into Series values({0},{1},'{2}',{3})", seriesSetting.tournamentId, seriesSetting.SportId, seriesSetting.tournamentName.Replace("'", "''"), seriesSetting.Status == true ? 1 : 0);
                    await _baseRepository.QueryAsync<Series>(sql);
                    _baseRepository.Commit();
                    //var _resultId = await _baseRepository.InsertAsync(seriesSetting);
                    //if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = true, Message = MessageStatus.Save, IsSuccess = true, Status = ResponseStatusCode.OK };
                }
                return new CommonReturnResponse { Data = null, Message = MessageStatus.NotExist, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> UpdateMatchSettingAsync(UpdateMatchVM matchSetting)
        {
            string sql = string.Empty;
            try
            {
                sql = $"Select * from SportsEvent where gameId = '{matchSetting.gameId}'";
                var sportsEvents = (await _baseRepository.QueryAsync<SportsEvent>(sql)).FirstOrDefault();
                if (sportsEvents != null)
                {
                    sportsEvents.DisableMatch = matchSetting.Status == true ? false : true;
                    sql = string.Format(@"Update SportsEvent set DisableMatch = {0} where eid = {1} and gameId = {2}", sportsEvents.DisableMatch == true ? 1 : 0, matchSetting.SportId, matchSetting.gameId);
                    await _baseRepository.QueryAsync<SportsEvent>(sql);

                    _baseRepository.Commit();
                    return new CommonReturnResponse { Data = true, Message = MessageStatus.Update, IsSuccess = true, Status = ResponseStatusCode.OK };
                }
                else
                {
                    return new CommonReturnResponse { Data = null, Message = MessageStatus.NotExist, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
                }

            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally
            {
                sql = string.Empty;
            }
        }

        public async Task<CommonReturnResponse> UpdateStakeLimitAsync(List<StakeLimit> stakeLimitList)
        {
            try
            {
                await _baseRepository.BulkUpdate(stakeLimitList);
                //_baseRepository.Commit();
                return new CommonReturnResponse { Data = true, Message = MessageStatus.Update, IsSuccess = true, Status = ResponseStatusCode.OK };
            }
            catch (Exception ex)
            {
                //_baseRepository.Rollback();
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetStakeLimitAsync()
        {
            try
            {
                var stakeLimits = await _baseRepository.GetListAsync<StakeLimit>();
                return new CommonReturnResponse
                {
                    Data = stakeLimits,
                    Message = stakeLimits.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = stakeLimits.Count > 0,
                    Status = stakeLimits.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
    }
}
