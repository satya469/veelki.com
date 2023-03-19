using Newtonsoft.Json;
using Veelki.Core.IServices;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Veelki.Core.Services
{
    public class RequestServices : IRequestServices
    {
        public string accessToken { get; set; }

        public void token(string token)
        {
            accessToken = token;
        }

        public TOut Get<TIn, TOut>(string uri)
        {
            using (var client = new WebClient())
            {
                if (accessToken != null && accessToken != "")
                {
                    client.Headers.Add("Authorization", "Bearer " + accessToken);
                }
                var responseBody = client.DownloadString(uri);
                return JsonConvert.DeserializeObject<TOut>(responseBody);
            }
        }

        public async Task<TOut> GetAsync<TOut>(string uri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                if (accessToken != null && accessToken != "")
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                }
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
        }

        //public string GetMarketAsync(string response)
        //{
        //    return JsonConvert.DeserializeObject<string>(response);
        //}

        public TOut Post<TIn, TOut>(string uri, TIn content)
        {
            using (var client = new WebClient())
            {
                client.BaseAddress = uri;
                client.Encoding = Encoding.UTF8;
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                if (accessToken != null && accessToken != "")
                {
                    client.Headers.Add("Authorization", "Bearer " + accessToken);
                }
                string data = JsonConvert.SerializeObject(content);
                var response = client.UploadString(uri, data);
                return JsonConvert.DeserializeObject<TOut>(response);
            }
        }

        public async Task<TOut> PostAsync<TIn, TOut>(string uri, TIn content)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (accessToken != null && accessToken != "")
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                }
                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(uri, serialized))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
        }
    }
}
