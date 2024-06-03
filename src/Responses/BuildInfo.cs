using System.Collections.Generic;

namespace jenkins_api_cs.Responses
{
    /// <summary>
    /// Main class for culprits. Who recently modified the build before breaking it?
    /// </summary>
    public class Culprits
    {
        /// <summary>
        /// The absoluteUrl to the culprit
        /// </summary>
        public string AbsoluteUrl { get; set; }
        /// <summary>
        /// The name of the culprit
        /// </summary>
        public string FullName { get; set; }
    }
    
    /// <summary>
    /// Instance class for specific build info
    /// </summary>
    public class BuildInfo
    {
        /// <summary>
        /// How long did the build run?
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// List of recent people who made modifications
        /// </summary>
        public List<Culprits> Culprits { get; set; }
        /// <summary>
        /// Full name of the build
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Absolute url to the build
        /// </summary>
        public string AbsoluteUrl { get; set; }
    }
}