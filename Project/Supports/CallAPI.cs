using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Supports
{
    public class CallAPI
    {
        public static string MethodGET(string URL)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(URL);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = httpClient.GetAsync("").Result;
                string content = result.Content.ReadAsStringAsync().Result.ToString();
                return content;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static string MethodPOST_Header(string URL, List<KeyValuePair<string, string>> parameter)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(URL);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                parameter.ForEach(s =>
                {
                    httpClient.DefaultRequestHeaders.Add(s.Key, s.Value);
                });
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
                var result = httpClient.SendAsync(request).Result;
                HttpStatusCode status = result.StatusCode;
                var content = result.Content.ReadAsStringAsync().Result;
                return content.ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static string MethodPOST_Body(string URL, Object ob)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(ob), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = httpClient.PostAsync(URL, content).Result;
                var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? result.ToString() : null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }


        public static string MethodPUT(string URL, Object ob)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(ob), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = httpClient.PutAsync(URL, content).Result;
                //if(httpResponseMessage.IsSuccessStatusCode)
                var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? result.ToString() : null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static bool MethodDELETE(string URL)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage httpResponseMessage = httpClient.DeleteAsync(URL).Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? true : false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}
