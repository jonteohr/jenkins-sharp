using jenkins_api_cs.Collections;
using Newtonsoft.Json;

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
    public class Build : ResponseBase
    {
        /// <summary>
        /// The incrementing build-number for this build
        /// </summary>
        public int Number { get; }
        /// <summary>
        /// The URL to this build
        /// </summary>
        public string Url { get; }
        
        [JsonConstructor]
        internal Build(int number, string url)
        {
            Number = number;
            Url = url;
        }
    }
    
    /// <summary>
    /// Main class of job information
    /// </summary>
    public class JobInfo : ResponseBase
    {
        /// <summary>
        /// The full name of the job.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// The name of the job (display-name).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The current color of the job.
        /// </summary>
        public BuildStatus Color { get; }
        /// <summary>
        /// The last build.
        /// </summary>
        public Build LastBuild { get; }
        /// <summary>
        /// The last failed build for this job.
        /// </summary>
        public Build LastFailedBuild { get; }
        /// <summary>
        /// The last successful build for this job.
        /// </summary>
        public Build LastSuccessfulBuild { get; }
        /// <summary>
        /// The URL to the job.
        /// </summary>
        public string Url { get; }
        /// <summary>
        /// The type of job this is.
        /// </summary>
        public JobType JobType { get; internal set; }
        /// <summary>
        /// A collection of sub-jobs contained in this job.
        /// If this is a folder, this collection contains all jobs within said folder.
        /// </summary>
        public JobCollection Jobs { get; }
        /// <summary>
        /// Collection of builds that has run
        /// </summary>
        public BuildCollection Builds { get; }
        /// <summary>
        /// The description set for this job
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// If this job is currently in queue to build
        /// </summary>
        public bool InQueue { get; }
        /// <summary>
        /// The number that will be used for the next build
        /// </summary>
        public int NextBuildNumber { get; }
        /// <summary>
        /// If this job is disabled
        /// </summary>
        public bool Disabled { get; }
        /// <summary>
        /// Jobs that will be triggered once this job has finished
        /// </summary>
        public JobCollection DownstreamProjects { get; }
        /// <summary>
        /// Jobs that trigger this job once they complete
        /// </summary>
        public JobCollection UpstreamProjects { get; }
        /// <summary>
        /// If the job allows concurrent builds
        /// </summary>
        public bool ConcurrentBuild { get; }

        [JsonConstructor]
        internal JobInfo(string name, string fullName, BuildStatus color, Build lastBuild, Build lastFailedBuild, Build lastSuccessfulBuild, string url, JobType jobType, JobCollection jobs, BuildCollection builds, string description, bool inQueue, int nextBuildNumber, bool disabled, JobCollection downstreamProjects, JobCollection upstreamProjects, bool concurrentBuild)
        {
            Name = name;
            FullName = fullName;
            Color = color;
            LastBuild = lastBuild;
            LastFailedBuild = lastFailedBuild;
            LastSuccessfulBuild = lastSuccessfulBuild;
            Url = url;
            JobType = jobType;
            Jobs = jobs;
            Builds = builds;
            Description = description;
            InQueue = inQueue;
            NextBuildNumber = nextBuildNumber;
            Disabled = disabled;
            DownstreamProjects = downstreamProjects;
            UpstreamProjects = upstreamProjects;
            ConcurrentBuild = concurrentBuild;
        }
    }
}