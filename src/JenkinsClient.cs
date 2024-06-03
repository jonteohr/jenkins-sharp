using System;
using System.Threading.Tasks;
using jenkins_api_cs.Collections;
using jenkins_api_cs.Responses;
using Newtonsoft.Json;

namespace jenkins_api_cs
{
    /// <summary>
    /// The main client class for accessing the Jenkins API
    /// </summary>
    public class JenkinsClient
    {
        private const string ApiEndString = "/api/json?pretty=true";
        /// <summary>
        /// The configured URL to the jenkins host
        /// </summary>
        public string JenkinsUrl { get; internal set; }

        /// <summary>
        /// Asynchronously gets the latest information regarding a specific job.
        /// </summary>
        /// <param name="jobName">The name of the job to fetch. If the job is put inside folders, you'll need to include them in the name.</param>
        /// <example>
        /// GetJobInfoAsync("My_job_name"); GetJobInfoAsync("folder/job/My_Job_name");
        /// </example>
        /// <returns>A <see cref="JobInfo"/> instance with data on the requested job</returns>
        /// <seealso cref="GetJobInfo"/>
        public async Task<JobInfo> GetJobInfoAsync(string jobName)
        {
            var apiUrl = JenkinsUrl + $"/job/{jobName}" + ApiEndString;
            var jobInfo = await HttpGet.Get<JobInfo>(apiUrl);
            
            return jobInfo;
        }
        
        /// <summary>
        /// Gets the latest information regarding a specific job.
        /// </summary>
        /// <param name="jobName">The name of the job to fetch. If the job is put inside folders, you'll need to include them in the name.</param>
        /// <example>
        /// GetJobInfoAsync("My_job_name"); GetJobInfoAsync("folder/job/My_Job_name");
        /// </example>
        /// <returns>A <see cref="JobInfo"/> instance with data on the requested job</returns>
        /// <seealso cref="GetJobInfoAsync"/>
        public JobInfo GetJobInfo(string jobName)
        {
            return GetJobInfoAsync(jobName).Result;
        }

        /// <summary>
        /// Asynchronously gets the latest information regarding a specific build inside a job.
        /// </summary>
        /// <param name="jobName">The name of the job to fetch a build from. If the job is put inside folders, you'll need to include them in the name.</param>
        /// <param name="buildNo">The number of the build</param>
        /// <returns>a <see cref="BuildInfo"/> instance with data of the requested build</returns>
        /// <seealso cref="GetBuildInfo"/>
        public async Task<BuildInfo> GetBuildInfoAsync(string jobName, int buildNo)
        {
            var apiUrl = JenkinsUrl + $"/job/{jobName}/{buildNo}" + ApiEndString;

            return await HttpGet.Get<BuildInfo>(apiUrl);
        }

        /// <summary>
        /// Gets the latest information regarding a specific build inside a job.
        /// </summary>
        /// <param name="jobName">The name of the job to fetch a build from. If the job is put inside folders, you'll need to include them in the name.</param>
        /// <param name="buildNo">The number of the build</param>
        /// <returns>a <see cref="BuildInfo"/> instance with data of the requested build</returns>
        /// <seealso cref="GetBuildInfoAsync"/>
        public BuildInfo GetBuildInfo(string jobName, int buildNo)
        {
            return GetBuildInfoAsync(jobName, buildNo).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<JobCollection> GetAllJobsAsync()
        {
            var apiUrl = JenkinsUrl + ApiEndString;

            var Coll = await HttpGet.Get<JobCollection>(apiUrl);

            Console.WriteLine(JsonConvert.SerializeObject(Coll));
            return Coll;
        }
    }

    /// <summary>
    /// A builder class assisting with building a <see cref="JenkinsClient"/>
    /// </summary>
    public class JenkinsClientBuilder
    {
        private string _url;
        
        /// <summary>
        /// Builds the client with the configured settings
        /// </summary>
        /// <returns>Configured instance of a <see cref="JenkinsClient"/></returns>
        /// <exception cref="ArgumentException">If the builder hasn't been configured enough before building.
        /// For example; not setting a URL to the jenkins instance.</exception>
        public JenkinsClient Build()
        {
            if (string.IsNullOrEmpty(_url))
            {
                throw new ArgumentException("No url to API was supplied.");
            }
            
            return new JenkinsClient { JenkinsUrl = _url };
        }

        /// <summary>
        /// Sets the URL to the requested jenkins instance to use. 
        /// </summary>
        /// <param name="url">A URL to the jenkins instance</param>
        /// <returns><see cref="JenkinsClientBuilder"/> to continue configuration</returns>
        /// <example>SetUrl("https://jenkins.jonteohr.xyz");</example>
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