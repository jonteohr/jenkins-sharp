using System;
using System.Net.Http;
using System.Threading.Tasks;
using jenkins_api_cs.Collections;
using jenkins_api_cs.Responses;
using Newtonsoft.Json.Linq;

namespace jenkins_api_cs.HttpRequests
{
    internal static class HttpRequest
    {
        private static async Task<string> Get(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
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
            var response = await Get(url);
            var apiResponse = (JArray) JObject.Parse(response)["jobs"];

            return JobCollection.FromJson(apiResponse);
        }

        internal static async Task<BuildInfo> GetBuildInfo(string url)
        {
            var response = await Get(url);
            var apiResponse = (JToken)JObject.Parse(response);
            
            return ResponseBase.FromJson<BuildInfo>(apiResponse);
        }

        internal static async Task<JobInfo> GetJobInfo(string url)
        {
            var response = await Get(url);
            var apiResponse = (JToken)JObject.Parse(response);
            
            return ResponseBase.FromJson<JobInfo>(apiResponse);
        }
    }
}