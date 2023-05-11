using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BetSettle
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at OnStart " + DateTime.Now);
            BetSettle();
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5 * 60 * 1000; //number in milisecinds
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
            BetSettle();
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        public void BetSettle()
        {
            CommonReturnResponse commonModel = null;
            try
            {
                commonModel = Get<CommonReturnResponse, CommonReturnResponse>("http://api.veelki.com/api/BetApi/BetSettle");
                if (commonModel.Data == null)
                {
                    WriteToFile($"Service call api and api gives null at {DateTime.Now}");
                }
                else
                {
                    WriteToFile($"{commonModel.Data}");
                }
                
            }
            catch (Exception ex)
            {
                WriteToFile($"Service gives error at {DateTime.Now} - {ex.Message}");
            }
        }

        public TOut Get<TIn, TOut>(string uri)
        {
            string responseBody = "";
            try
            {
                //using (var client = new WebClient())
                //{
                //    responseBody = client.DownloadString(uri);
                //    return JsonConvert.DeserializeObject<TOut>(responseBody);
                //}
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(uri).GetAwaiter().GetResult();
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<TOut>(result);
                    // Do stuff...
                }
            }
            catch (Exception ex)
            {
                WriteToFile($"Service gives error at Get request {DateTime.Now} - {ex.Message}");
                return JsonConvert.DeserializeObject<TOut>(responseBody);
            }            
        }
    }
}
