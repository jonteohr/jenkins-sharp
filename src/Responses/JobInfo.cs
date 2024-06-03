using System;
using System.Linq;
using jenkins_api_cs.Collections;
using Newtonsoft.Json.Linq;

namespace jenkins_api_cs.Responses
{
    /// <summary>
    /// The current status of the build, in color
    /// </summary>
    public enum BuildStatus
    {
        Disabled,
        Notbuilt,
        Red,
        Blue,
        Yellow,
        Grey,
        Red_anime,
        Blue_anime,
        Yellow_anime,
        Grey_anime
    }

    /// <summary>
    /// Jobs have different types. This indicates which category the specific job is under.
    /// </summary>
    public enum JobType
    {
        /// <summary>
        /// If the job type was not able to parse
        /// </summary>
        Unknown,
        /// <summary>
        /// A pipeline job
        /// </summary>
        WorkflowJob,
        /// <summary>
        /// A freestyle project job
        /// </summary>
        FreeStyleProject,
        /// <summary>
        /// A regular, non-buildable, folder containing other jobs
        /// </summary>
        Folder,
        /// <summary>
        /// Multibranch pipeline job
        /// </summary>
        WorkflowMultiBranchProject
    }

    public class Build
    {
        public int Number { get; set; }
        public string Url { get; set; }
    }
    
    public class JobInfo
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public BuildStatus Color { get; set; }
        public Build LastBuild { get; set; }
        public Build LastFailedBuild { get; set; }
        public Build LastSuccessfulBuild { get; set; }
        public string Url { get; set; }
        public JobType JobType { get; set; }
        public JobCollection Jobs { get; set; }

        internal static JobInfo FromJson(JToken json)
        {
            var _class = json["_class"]?.ToString().Split('.').Last();
            if (!Enum.TryParse(_class, out JobType jobtype))
                jobtype = JobType.Unknown;
                
            var jobi = json.ToObject<JobInfo>();
            jobi.JobType = jobtype;

            return jobi;
        }
    }
}