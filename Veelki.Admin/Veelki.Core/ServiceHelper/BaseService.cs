namespace Veelki.Core.ServiceHelper
{
    public class BaseService
    {
        public virtual ServiceResponse<T> SetResultStatus<T>(T objData, string Message, bool IsSuccess, int StatusCode = 0, string validationMessage = "") where T : class
        {
            ServiceResponse<T> objReturn = new ServiceResponse<T>();
            objReturn.Message = Message;
            objReturn.IsSuccess = IsSuccess;
            objReturn.Data = objData;
            objReturn.StatusCode = StatusCode;
            return objReturn;
        }
    }
}
