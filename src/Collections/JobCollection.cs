using System.Collections.Generic;
using System.Linq;
using jenkins_api_cs.Responses;
using Newtonsoft.Json.Linq;

namespace jenkins_api_cs.Collections
{
    /// <summary>
    /// Collection of <see cref="JobInfo"/> instances
    /// </summary>
    public class JobCollection : List<JobInfo>
    {
        internal static JobCollection FromJson(JArray jsonObject)
        {
            var result = new JobCollection();
            result.AddRange(jsonObject.Select(JobInfo.FromJson));

            return result;
        }
    }
}