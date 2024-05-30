using System.Collections.Generic;

namespace jenkins_api_cs.Responses
{
    public class Culprits
    {
        public string AbsoluteUrl { get; set; }
        public string FullName { get; set; }
    }
    public class BuildInfo
    {
        public int Duration { get; set; }
        public List<Culprits> Culprits { get; set; }
        public string FullName { get; set; }
        public string AbsoluteUrl { get; set; }
    }
}