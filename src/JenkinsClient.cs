using System;
using System.Threading.Tasks;
using jenkins_api_cs.Responses;

namespace jenkins_api_cs
{
    public class JenkinsClient
    {
        public string JenkinsUrl { get; internal set; }

        public async Task<JobInfo> GetJobInfo(string jobName)
        {
            var apiUrl = JenkinsUrl + $"/job/{jobName}/api/json?pretty=true";

            return await HttpGet.Get<JobInfo>(apiUrl);
        }

        public async Task<BuildInfo> GetBuildInfo(string jobName, int buildNo)
        {
            var apiUrl = JenkinsUrl + $"/job/{jobName}/{buildNo}/api/json?pretty=true";

            return await HttpGet.Get<BuildInfo>(apiUrl);
        }
    }

    public class JenkinsClientBuilder
    {
        private string _url;
        
        public JenkinsClient Build()
        {
            if (string.IsNullOrEmpty(_url))
            {
                throw new ArgumentException("No url to API was supplied.");
            }
            
            return new JenkinsClient { JenkinsUrl = _url };
        }

        public JenkinsClientBuilder SetUrl(string url)
        {
            var fixedUrl = url;
            if (fixedUrl.EndsWith("/"))
                fixedUrl = fixedUrl.Remove(url.Length - 1, 1);
            _url = fixedUrl;
            
            return this;
        }
    }
}