using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Veelki.Core.ServiceHelper
{
    public class cLogger
    {
        
        IConfiguration _configuration;
        string _logDir = string.Empty;

        public cLogger()
        {
            //_configuration = configuration;
            //_logDir = Convert.ToString(_configuration["LOGPATH"]);
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
                    using (StreamWriter oStreamWriter = File.CreateText(_logFilePath))
                    {
                        oStreamWriter.WriteLine(string.Format("{0}|{1}", DateTime.Now, _errMsg));
                    }
                }
                else
                {
                    using (StreamWriter oStreamWriter = File.AppendText(_logFilePath))
                    {
                        oStreamWriter.WriteLine(string.Format("{0}|{1}", DateTime.Now, _errMsg));
                    }
                }
            }
            catch (Exception ex)
            {
                LogException("WriteLogFile() Exception:", ex);
            }
        }
        public void LogException(string message, Exception ex)
        {
            WriteLogFile(string.Format("{2}::Exception: Message:{0}\text:{1}", message, ex.ToString(), DateTime.Now.ToString("dd-MM-yyyy-hh:mm:ss")));
        }
    }
}
