using jenkins_api_cs.Authentication;
using jenkins_api_cs.Collections;
using jenkins_api_cs.Exceptions;
using jenkins_api_cs.Responses;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace jenkins_api_cs.HttpRequests
{
    internal static class HttpRequest
    {
        private static async Task<string> Get(string url, JenkinsCredentials credentials = null)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if (credentials != null)
                    {
                        var byteArray = new ASCIIEncoding().GetBytes($"{credentials.Username}:{credentials.ApiKey}");
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    }
                    var response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
                catch (HttpRequestException ex)
                {
                    throw new JobNotFoundException(ex.Message, ex);
                }
                catch (Exception ex)
                {
                    throw new JenkinsException(ex.Message, ex);
                }
            }
        }

        internal static async Task<JobCollection> GetJobs(string url, JenkinsCredentials credentials)
        {
            var response = await Get(url, credentials);
            var apiResponse = (JArray) JObject.Parse(response)["jobs"];

            return JobCollection.FromJson(apiResponse);
        }

        internal static async Task<BuildInfo> GetBuildInfo(string url, JenkinsCredentials credentials)
        {
            var response = await Get(url, credentials);
            var apiResponse = (JToken)JObject.Parse(response);
            
            return ResponseBase.FromJson<BuildInfo>(apiResponse);
        }

        internal static async Task<JobInfo> GetJobInfo(string url, JenkinsCredentials credentials)
        {
            var response = await Get(url, credentials);
            var apiResponse = (JToken)JObject.Parse(response);
            
            return ResponseBase.FromJson<JobInfo>(apiResponse);
        }
    }
}