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
        /// <summary>
        /// Job has failed
        /// </summary>
        Red,
        /// <summary>
        /// Job has failed and is now re-building
        /// </summary>
        Red_Anime,
        /// <summary>
        /// Job is unstable
        /// </summary>
        Yellow,
        /// <summary>
        /// Job is unstable and is now re-building
        /// </summary>
        Yellow_Anime,
        /// <summary>
        /// Job is stable
        /// </summary>
        Blue,
        /// <summary>
        /// Job is stable and re-building
        /// </summary>
        Blue_Anime,
        /// <summary>
        /// Job has no previous status
        /// </summary>
        Grey,
        /// <summary>
        /// Job has no previous state and is now re-building.
        /// Could be first build!
        /// </summary>
        Grey_Anime,
        /// <summary>
        /// Job is disabled
        /// </summary>
        Disabled,
        /// <summary>
        /// Job is disabled but is still building
        /// </summary>
        Disabled_Anime,
        /// <summary>
        /// Job was aborted
        /// </summary>
        Aborted,
        /// <summary>
        /// Job was aborted but is re-building
        /// </summary>
        Aborted_Anime,
        /// <summary>
        /// Job has not run yet
        /// </summary>
        Notbuilt,
        /// <summary>
        /// Job is running its first build
        /// </summary>
        Notbuilt_Anime
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

    /// <summary>
    /// Data class for specific builds
    /// </summary>
    public class Build
    {
        /// <summary>
        /// The incrementing build-number for this build
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// The URL to this build
        /// </summary>
        public string Url { get; set; }
    }
    
    /// <summary>
    /// Main class of job information
    /// </summary>
    public class JobInfo
    {
        /// <summary>
        /// The full name of the job.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// The name of the job (display-name).
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The current color of the job.
        /// </summary>
        public BuildStatus Color { get; set; }
        /// <summary>
        /// The last build.
        /// </summary>
        public Build LastBuild { get; set; }
        /// <summary>
        /// The last failed build for this job.
        /// </summary>
        public Build LastFailedBuild { get; set; }
        /// <summary>
        /// The last successful build for this job.
        /// </summary>
        public Build LastSuccessfulBuild { get; set; }
        /// <summary>
        /// The URL to the job.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The type of job this is.
        /// </summary>
        public JobType JobType { get; set; }
        /// <summary>
        /// A collection of sub-jobs contained in this job.
        /// If this is a folder, this collection contains all jobs within said folder.
        /// </summary>
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