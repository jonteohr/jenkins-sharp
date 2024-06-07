using System;
using System.Net.Http;
using System.Threading.Tasks;
using jenkins_api_cs.Collections;
using jenkins_api_cs.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace jenkins_api_cs.HttpRequests
{
    internal static class HttpRequest
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
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        internal static async Task<JobCollection> GetJobs(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    var json = (JArray) JObject.Parse(responseBody)["jobs"];
                    
                    return JobCollection.FromJson(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        internal static async Task<JobInfo> GetJobInfo(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var apiResponse = (JToken) JObject.Parse(responseBody);

                    return JobInfo.FromJson(apiResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}