using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;

namespace VeelkiSportsEvent
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
            WriteToFile("Service is started at " + DateTime.Now);
            SaveSportsEvent();
            WriteToFile("Insert Sorts Event at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 10 * 60 * 1000; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {            
            WriteToFile("Service is recall at " + DateTime.Now);
            SaveSportsEvent();
            WriteToFile("Insert Sorts Event at " + DateTime.Now);
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

        public void SaveSportsEvent()
        {
            CommonReturnResponse commonModel = null;
            try
            {
                commonModel = Get<CommonReturnResponse, CommonReturnResponse>("http://api.88cricx.com/api/exchange/GetSportsEventsForWindowService");
            }
            catch (Exception ex)
            {
                WriteToFile($"Service gives error at {DateTime.Now} - {ex.Message}");
            }
        }

        public TOut Get<TIn, TOut>(string uri)
        {
            using (var client = new WebClient())
            {
                var responseBody = client.DownloadString(uri);
                return JsonConvert.DeserializeObject<TOut>(responseBody);
            }
        }
    }
}
