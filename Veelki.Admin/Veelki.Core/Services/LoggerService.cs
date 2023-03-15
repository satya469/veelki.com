using Microsoft.Extensions.Configuration;
using Veelki.Core.IServices;
using System;
using System.IO;

namespace Veelki.Core.Services
{
    public class LoggerService : ILoggerService
    {
        IConfiguration _configuration;
        string _logDir = string.Empty;

        public LoggerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _logDir = Convert.ToString(_configuration["LOGPATH"]);
        }

        private void WriteLogFile(string _errMsg)
        {
            string _logFilePath = string.Empty;
            try
            {
                if (!Directory.Exists(_logDir)) { Directory.CreateDirectory(_logDir); }
                _logFilePath = Path.Combine(_logDir, string.Format("{0:yyyy-MM-dd}.txt", DateTime.Now));

                if (!File.Exists(_logFilePath))
                {
                    using (StreamWriter oStreamWriter = File.CreateText(_logFilePath)) { oStreamWriter.WriteLine(string.Format("{0}|{1}", DateTime.Now, _errMsg)); }
                }
                else
                {
                    using (StreamWriter oStreamWriter = File.AppendText(_logFilePath)) { oStreamWriter.WriteLine(string.Format("{0}|{1}", DateTime.Now, _errMsg)); }
                }
            }
            catch (Exception ex) { LogException("WriteLogFile() Exception:", ex); }
        }
        public void LogException(string message, Exception ex)
        {
            WriteLogFile(string.Format("{2}::Exception: Message:{0}\text:{1}", message, ex.ToString(), DateTime.Now.ToString("dd-MM-yyyy-hh:mm:ss")));
        }
    }
}
