using System.Collections.Generic;
using Newtonsoft.Json;

namespace jenkins_api_cs.Responses
{
    /// <summary>
    /// Main class for culprits. Who recently modified the build before breaking it?
    /// </summary>
    public class Culprits : ResponseBase
    {
        /// <summary>
        /// The absoluteUrl to the culprit
        /// </summary>
        public string AbsoluteUrl { get; }
        /// <summary>
        /// The name of the culprit
        /// </summary>
        public string FullName { get; }
        
        [JsonConstructor]
        internal Culprits(string absoluteUrl, string fullName)
        {
            AbsoluteUrl = absoluteUrl;
            FullName = fullName;
        }
    }
    
    /// <summary>
    /// Instance class for specific build info
    /// </summary>
    public class BuildInfo : ResponseBase
    {
        /// <summary>
        /// How long did the build run?
        /// </summary>
        public int Duration { get; }
        /// <summary>
        /// List of recent people who made modifications
        /// </summary>
        public List<Culprits> Culprits { get; }
        /// <summary>
        /// Full name of the build
        /// </summary>
        public string FullName { get; }
        /// <summary>
        /// Absolute url to the build
        /// </summary>
        public string AbsoluteUrl { get; }
        /// <summary>
        /// The result of the build
        /// </summary>
        public string Result { get; }
        /// <summary>
        /// The build agent used for this build
        /// </summary>
        public string BuiltOn { get; }
        /// <summary>
        /// If this build is currently building or not
        /// </summary>
        public bool Building { get; }
        
        [JsonConstructor]
        internal BuildInfo(bool building, string builtOn, string result, string absoluteUrl, string fullName, List<Culprits> culprits, int duration)
        {
            Building = building;
            BuiltOn = builtOn;
            Result = result;
            AbsoluteUrl = absoluteUrl;
            FullName = fullName;
            Culprits = culprits;
            Duration = duration;
        }
    }
}