﻿using jenkins_api_cs.Authentication;
using jenkins_api_cs.Collections;
using jenkins_api_cs.Exceptions;
using jenkins_api_cs.HttpRequests;
using jenkins_api_cs.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jenkins_api_cs
{
    /// <summary>
    /// The main client class for accessing the Jenkins API
    /// </summary>
    public class JenkinsClient
    {
        private const string ApiEndString = "/api/json?pretty=true";
        private JenkinsCredentials Credentials { get; }
        /// <summary>
        /// The configured URL to the jenkins host
        /// </summary>
        public string JenkinsUrl { get; }

        /// <summary>
        /// Returns true if credentials has been configured
        /// </summary>
        public bool WithCredentials => Credentials != null;

        /// <summary>
        /// Main class constructor for the client
        /// </summary>
        /// <exception cref="JenkinsException">The client was not setup with an url to the jenkins instance</exception>
        internal JenkinsClient(string url, JenkinsCredentials credentials = null)
        {
            if (string.IsNullOrEmpty(url)) // If the client was set up without proper configuration
                throw new JenkinsException("No url to the jenkins instance was supplied.", null);

            JenkinsUrl = url;
            Credentials = credentials;
        }
        
        /// <summary>
        /// Asynchronously gets the latest information regarding a specific job.
        /// </summary>
        /// <param name="jobName">The name of the job to fetch. If the job is put inside folders, you'll need to include them in the name.</param>
        /// <example>
        /// GetJobInfoAsync("My_job_name"); GetJobInfoAsync("folder/job/My_Job_name");
        /// </example>
        /// <returns>A <see cref="JobInfo"/> instance with data on the requested job</returns>
        /// <seealso cref="GetJobInfo(string)"/>
        public async Task<JobInfo> GetJobInfoAsync(string jobName)
        {
            var apiUrl = JenkinsUrl + $"/job/{jobName}" + ApiEndString;
            var jobInfo = await HttpRequest.GetJobInfo(apiUrl, Credentials);
            
            return jobInfo;
        }

        /// <summary>
        /// Asynchronously gets the latest information regarding several jobs.
        /// </summary>
        /// <param name="jobs">The name of the job to fetch. If the job is put inside folders, you'll need to include them in the name.</param>
        /// <example>
        /// GetJobInfoAsync("My_job_name", "Another_Job"); GetJobInfoAsync("folder/job/My_Job_name", "Production/Job/A_Job_Name");
        /// </example>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> instance with data on the requested jobs</returns>
        /// <seealso cref="GetJobInfoAsync(string)"/>
        public async Task<Dictionary<string, JobInfo>> GetJobInfoAsync(params string[] jobs)
        {
            var dict = new Dictionary<string, JobInfo>();
            foreach (var job in jobs)
            {
                dict.Add(job, await GetJobInfoAsync(job));
            }

            return dict;
        }
        
        /// <summary>
        /// Gets the latest information regarding a specific job.
        /// </summary>
        /// <param name="jobName">The name of the job to fetch. If the job is put inside folders, you'll need to include them in the name.</param>
        /// <example>
        /// GetJobInfoAsync("My_job_name"); GetJobInfoAsync("folder/job/My_Job_name");
        /// </example>
        /// <returns>A <see cref="JobInfo"/> instance with data on the requested job</returns>
        /// <seealso cref="GetJobInfoAsync(string)"/>
        public JobInfo GetJobInfo(string jobName)
        {
            return GetJobInfoAsync(jobName).Result;
        }

        /// <summary>
        /// Gets the latest information regarding a specific job.
        /// </summary>
        /// <param name="jobs">The names of the jobs to fetch. If the job is put inside folders, you'll need to include them in the name.</param>
        /// <example>
        /// GetJobInfo("My_job_name", "Another_Job"); GetJobInfo("folder/job/My_Job_name", "Production/Job/A_Job_Name");
        /// </example>
        /// <returns>A <see cref="JobInfo"/> instance with data on the requested job</returns>
        /// <seealso cref="GetJobInfoAsync(string)"/>
        public Dictionary<string, JobInfo> GetJobInfo(params string[] jobs)
        {
            return jobs.ToDictionary(job => job, GetJobInfo);
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

            return await HttpRequest.GetBuildInfo(apiUrl, Credentials);
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
        /// Asynchronously get all jobs on the jenkins server
        /// </summary>
        /// <returns>A <see cref="JobCollection"/> containing <see cref="JobInfo"/> instances filled with data on all jobs</returns>
        /// <seealso cref="GetAllJobsAsync(string)"/>
        /// <seealso cref="GetAllJobs(string)"/>
        /// <seealso cref="GetAllJobs()"/>
        public async Task<JobCollection> GetAllJobsAsync()
        {
            var apiUrl = JenkinsUrl + ApiEndString;

            return await HttpRequest.GetJobs(apiUrl, Credentials);
        }
        
        /// <summary>
        /// Asynchronously get all jobs in a specified folder
        /// </summary>
        /// <param name="folderName">Name of the folder to traverse.</param>
        /// <returns>A <see cref="JobCollection"/> containing <see cref="JobInfo"/> instances filled with data on all jobs</returns>
        /// <seealso cref="GetAllJobsAsync()"/>
        /// <seealso cref="GetAllJobs()"/>
        /// <seealso cref="GetAllJobs(string)"/>
        public async Task<JobCollection> GetAllJobsAsync(string folderName)
        {
            var apiUrl = JenkinsUrl + $"/job/{folderName}" + ApiEndString;

            return await HttpRequest.GetJobs(apiUrl, Credentials);
        }

        /// <summary>
        /// Asynchronously fetches all jobs within a specific view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <returns>A <see cref="JobCollection"/> containing <see cref="JobInfo"/> instances filled with data on all jobs</returns>
        /// <seealso cref="GetViewJobs"/>
        /// <seealso cref="GetUserViewJobsAsync"/>
        public async Task<JobCollection> GetViewJobsAsync(string viewName)
        {
            var apiUrl = JenkinsUrl + $"/view/{viewName}" + ApiEndString;

            return await HttpRequest.GetJobs(apiUrl, Credentials);
        }

        /// <summary>
        /// Fetches all jobs within a specific view
        /// </summary>
        /// <param name="viewName">The name of the view</param>
        /// <returns>A <see cref="JobCollection"/> containing <see cref="JobInfo"/> instances filled with data on all jobs</returns>
        /// <seealso cref="GetViewJobsAsync"/>
        public JobCollection GetViewJobs(string viewName)
        {
            return GetViewJobsAsync(viewName).Result;
        }

        /// <summary>
        /// Asynchronously fetches all jobs within a specific users custom views
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="view">The name of the view to fetch</param>
        /// <returns>A <see cref="JobCollection"/> containing <see cref="JobInfo"/> instances filled with data on all jobs</returns>
        /// <seealso cref="GetViewJobsAsync"/>
        public async Task<JobCollection> GetUserViewJobsAsync(string username, string view)
        {
            var apiUrl = JenkinsUrl + $"/user/{username}/my-views/view/{view}" + ApiEndString;

            return await HttpRequest.GetJobs(apiUrl, Credentials);
        }
        
        /// <summary>
        /// Fetches all jobs within a specific users custom views
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="view">The name of the view to fetch</param>
        /// <returns>A <see cref="JobCollection"/> containing <see cref="JobInfo"/> instances filled with data on all jobs</returns>
        /// <seealso cref="GetViewJobsAsync"/>
        public JobCollection GetUserViewJobs(string username, string view)
        {
            return GetUserViewJobsAsync(username, view).Result;
        }
        
        /// <summary>
        /// Get all jobs on the jenkins server
        /// </summary>
        /// <returns>A <see cref="JobCollection"/> containing <see cref="JobInfo"/> instances filled with data on all jobs</returns>
        /// <seealso cref="GetAllJobs(string)"/>
        /// <seealso cref="GetAllJobsAsync()"/>
        /// <seealso cref="GetAllJobsAsync(string)"/>
        public JobCollection GetAllJobs()
        {
            return GetAllJobsAsync().Result;
        }
        
        /// <summary>
        /// Asynchronously get all jobs in a specified folder
        /// </summary>
        /// <param name="folderName">Name of the folder to traverse.</param>
        /// <returns>A <see cref="JobCollection"/> containing <see cref="JobInfo"/> instances filled with data on all jobs</returns>
        /// <seealso cref="GetAllJobs()"/>
        /// <seealso cref="GetAllJobsAsync()"/>
        /// <seealso cref="GetAllJobsAsync(string)"/>
        public JobCollection GetAllJobs(string folderName)
        {
            return GetAllJobsAsync(folderName).Result;
        }
    }

    /// <summary>
    /// A builder class assisting with building a <see cref="JenkinsClient"/>
    /// </summary>
    public class JenkinsClientBuilder
    {
        private string _url;
        private JenkinsCredentials _credentials = null;
        
        /// <summary>
        /// Builds the client with the configured settings
        /// </summary>
        /// <returns>Configured instance of a <see cref="JenkinsClient"/></returns>
        /// <exception cref="JenkinsException">If the builder hasn't been configured enough before building.
        /// For example; not setting a URL to the jenkins instance.</exception>
        public JenkinsClient Build()
        {
            if (string.IsNullOrEmpty(_url))
            {
                throw new JenkinsException("No url to the jenkins instance was supplied.", null);
            }
            
            return new JenkinsClient(_url, _credentials);
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

        /// <summary>
        /// Sets the credentials to use when communicating with the Jenkins instance.
        /// This is optional.
        /// </summary>
        /// <param name="credentials">The credentials to use</param>
        /// <returns><see cref="JenkinsClientBuilder"/> to continue configuration</returns>
        public JenkinsClientBuilder WithCredentials(JenkinsCredentials credentials)
        {
            _credentials = credentials;
            return this;
        }
    }
}