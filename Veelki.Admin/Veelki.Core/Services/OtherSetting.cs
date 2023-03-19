using Veelki.Core.IServices;
using Veelki.Core.ServiceHelper;
using Veelki.Data.Entities;
using Veelki.Data.Repository;
using Veelki.Models.Model;
using System;
using System.Threading.Tasks;

namespace Veelki.Core.Services
{
    public class OtherSetting : IOtherSetting
    {
        private readonly IBaseRepository _baseRepository;

        public OtherSetting(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CommonReturnResponse> AddUpdateLogoAsync(Logo model)
        {
            Logo _logo = null;
            try
            {
                if (model.Id > 0)
                {
                    _logo = await _baseRepository.GetDataByIdAsync<Logo>(model.Id);
                    if (_logo != null)
                    {
                        model.FilePath = model.FilePath != null ? model.FilePath : _logo.FilePath;
                        model.FileName = model.FileName != null ? model.FileName : _logo.FileName;

                        int _resultId = await _baseRepository.UpdateAsync(model);
                        if(_resultId > 0)
                        {
                            if (model.Status == true)
                            {
                                string sql = string.Format(@"update Logo set Status = 0 where Id <> {0}", model.Id);
                                var logoList = await _baseRepository.QueryAsync<Logo>(sql);
                                _baseRepository.Commit();
                            }
                            else
                            {
                                _baseRepository.Commit();
                            }
                        }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Update : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                    else
                    {
                        return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
                    }
                }
                else
                {                   
                    var _result = await _baseRepository.InsertAsync(model);
                    if(_result <= 0)
                    {
                        _baseRepository.Rollback();
                    }
                    else
                    {
                        if (model.Status == true)
                        {
                            string sql = string.Format(@"update Logo set Status = 0 where Id <> {0}", _result);
                            var logoList = await _baseRepository.QueryAsync<Logo>(sql);
                            _baseRepository.Commit();
                        }
                        else
                        {
                            _baseRepository.Commit();
                        }
                    }
                    return new CommonReturnResponse { Data = _result > 0, Message = _result > 0 ? MessageStatus.Create : MessageStatus.Error, IsSuccess = _result > 0, Status = _result > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                }
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : CurrencyService : AddUpdatedAsync()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally
            {
                if (_logo != null) { _logo = null; }
            }
        }

        public async Task<CommonReturnResponse> AddUpdateNewsAsync(News model)
        {
            News _news = null;
            try
            {
                if (model.Id > 0)
                {
                    _news = await _baseRepository.GetDataByIdAsync<News>(model.Id);
                    if (_news != null)
                    {
                        int _resultId = await _baseRepository.UpdateAsync(model);
                        if(_resultId > 0)
                        {
                            if (model.Status == true)
                            {
                                string sql = string.Format(@"update News set Status = 0 where Id <> {0}", model.Id);
                                var logoList = await _baseRepository.QueryAsync<News>(sql);
                                _baseRepository.Commit();
                            }
                            else
                            {
                                _baseRepository.Commit();
                            }
                        }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Update : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                    else
                    {
                        return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
                    }
                }
                else
                {
                    var _result = await _baseRepository.InsertAsync(model);
                    if (_result <= 0)
                    {
                        _baseRepository.Rollback();
                    }
                    else
                    {
                        if (model.Status == true)
                        {
                            string sql = string.Format(@"update News set Status = 0 where Id <> {0}", _result);
                            var logoList = await _baseRepository.QueryAsync<News>(sql);
                            _baseRepository.Commit();
                        }
                        else
                        {
                            _baseRepository.Commit();
                        }
                    }
                    return new CommonReturnResponse { Data = _result > 0, Message = _result > 0 ? MessageStatus.Create : MessageStatus.Error, IsSuccess = _result > 0, Status = _result > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                }
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : CurrencyService : AddUpdatedAsync()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally
            {
                if (_news != null) { _news = null; }
            }
        }

        public async Task<CommonReturnResponse> AddUpdateSliderAsync(Slider model)
        {
            Slider _slider = null;
            try
            {
                if (model.Id > 0)
                {
                    _slider = await _baseRepository.GetDataByIdAsync<Slider>(model.Id);
                    if (_slider != null)
                    {
                        model.FilePath = model.FilePath != null ? model.FilePath : _slider.FilePath;
                        model.FileName = model.FileName != null ? model.FileName : _slider.FileName;
                        int _resultId = await _baseRepository.UpdateAsync(model);
                        if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Update : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                    else
                    {
                        return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
                    }
                }
                else
                {
                    var _result = await _baseRepository.InsertAsync(model);
                    if (_result > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = _result > 0, Message = _result > 0 ? MessageStatus.Create : MessageStatus.Error, IsSuccess = _result > 0, Status = _result > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                }
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : CurrencyService : AddUpdatedAsync()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally
            {
                if (_slider != null) { _slider = null; }
            }
        }

        public async Task<CommonReturnResponse> DeleteLogoAsync(int id)
        {
            Logo _logo = null;
            try
            {
                if (id > 0)
                {
                    _logo = await _baseRepository.GetDataByIdAsync<Logo>(id);
                    if (_logo != null)
                    {
                        int _resultId = await _baseRepository.DeleteAsync<Logo>(id);
                        if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Delete : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                }
                return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AircraftService : DeleteAsync()", ex);
                return new CommonReturnResponse()
                {
                    IsSuccess = false,
                    Status = ResponseStatusCode.EXCEPTION,
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                    Data = null
                };
            }
            finally
            {
                if (_logo != null) { _logo = null; }
            }
        }

        public async Task<CommonReturnResponse> DeleteNewsAsync(int id)
        {
            News news = null;
            try
            {
                if (id > 0)
                {
                    news = await _baseRepository.GetDataByIdAsync<News>(id);
                    if (news != null)
                    {
                        int _resultId = await _baseRepository.DeleteAsync<News>(id);
                        if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Delete : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                }
                return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AircraftService : DeleteAsync()", ex);
                return new CommonReturnResponse()
                {
                    IsSuccess = false,
                    Status = ResponseStatusCode.EXCEPTION,
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                    Data = null
                };
            }
            finally
            {
                if (news != null) { news = null; }
            }
        }

        public async Task<CommonReturnResponse> DeleteSliderAsync(int id)
        {
            Slider slider = null;
            try
            {
                if (id > 0)
                {
                    slider = await _baseRepository.GetDataByIdAsync<Slider>(id);
                    if (slider != null)
                    {
                        int _resultId = await _baseRepository.DeleteAsync<Slider>(id);
                        if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                        return new CommonReturnResponse { Data = null, Message = _resultId > 0 ? MessageStatus.Delete : MessageStatus.Error, IsSuccess = _resultId > 0, Status = _resultId > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
                    }
                }
                return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AircraftService : DeleteAsync()", ex);
                return new CommonReturnResponse()
                {
                    IsSuccess = false,
                    Status = ResponseStatusCode.EXCEPTION,
                    Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                    Data = null
                };
            }
            finally
            {
                if (slider != null) { slider = null; }
            }
        }
    }
}
