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
    }
}