using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jenkins_api_cs
{
    internal static class HttpGet
    {
        internal static async Task<T> Get<T>(string url) where T : new()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonConvert.DeserializeObject<T>(responseBody);

                    return apiResponse;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new T();
                }
            }
        }
    }
}