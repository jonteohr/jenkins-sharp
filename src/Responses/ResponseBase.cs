using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace jenkins_api_cs.Responses
{
    /// <summary>
    /// Base class for response classes
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Converts json <see cref="JToken"/> to a response class
        /// </summary>
        /// <param name="json">Json JToken</param>
        /// <typeparam name="T">The type class to be converted into</typeparam>
        /// <returns>A converted response class</returns>
        internal static T FromJson<T>(JToken json)
        {
            var buildInfo = json.ToObject<T>();
            
            return buildInfo;
        }

        internal static JobInfo JobInfoFromJson(JToken json)
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