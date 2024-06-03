namespace jenkins_api_cs.Responses
{
    public enum BuildStatus
    {
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
        public BuildStatus Color { get; set; }
        public Build LastBuild { get; set; }
        public Build LastFailedBuild { get; set; }
        public Build LastSuccessfulBuild { get; set; }
        public string Url { get; set; }
        public JobType JobType { get; set; }
    }
}