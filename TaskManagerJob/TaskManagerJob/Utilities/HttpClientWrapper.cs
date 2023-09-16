using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace TaskManagerJob.Utilities
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContext;


        public HttpClientWrapper(IConfiguration configuration, IHttpClientFactory clientFactory, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _httpContext = httpContext;
        }


        //public async Task<T> SendPostEmailRequest<T, U>(PostRequest<U> request)
        //{
        //    var client = _clientFactory.CreateClient();
        //    var message = new HttpRequestMessage();
        //    message.RequestUri = new Uri(request.Url);
        //    message.Method = HttpMethod.Post;
        //    message.Headers.Add("Accept", "application/json");
        //    //message.Headers.Add("x-clientid", "internal");
        //    //message.Headers.Add("x-channel", "internal");
        //    client.DefaultRequestHeaders.Clear();
        //    var data = JsonConvert.SerializeObject(request.Data);
        //    message.Content = new StringContent(data, Encoding.UTF8, "application/json");
        //    var response = await client.SendAsync(message);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<T>(content);
        //}

        public async Task<T> SendPostEmailRequest<T, U>(PostRequest<U> request)
        {
            var client = _clientFactory.CreateClient();
            var message = new HttpRequestMessage();
            message.RequestUri = new Uri(request.Url);
            message.Method = HttpMethod.Post;
            message.Headers.Add("Accept", "application/json");
            //message.Headers.Add("x-clientid", "internal");
            //message.Headers.Add("x-channel", "internal");
            client.DefaultRequestHeaders.Clear();
            var data = JsonConvert.SerializeObject(request.Data);
            message.Content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(message);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        /* public T SendPostEmailAsync<T>(string baseUrl, object body)
         {
             try
             {
                 using (var _httpClient = new System.Net.Http.HttpClient())
                 {
                     _httpClient.BaseAddress = new Uri(baseUrl);

                     var json = JsonConvert.SerializeObject(body);
                     var content = new StringContent(json, Encoding.UTF8, "application/json");
                     var _response = _httpClient.PostAsync(baseUrl, content).Result;
                     if (_response.IsSuccessStatusCode)
                     {
                         var _content1 = _response.Content.ReadAsStringAsync().Result;
                         var Item = JsonConvert.DeserializeObject<T>(_content1);
                         return Item;
                     }
                     else
                     {
                         Console.WriteLine(_response);
                         throw new Exception(_response.Content.ReadAsStringAsync().Result);
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }

     }*/
    }
}
