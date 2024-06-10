using System.Collections.Generic;
using System.Linq;
using jenkins_api_cs.Responses;
using Newtonsoft.Json.Linq;

namespace jenkins_api_cs.Collections
{
    /// <summary>
    /// Collection of <see cref="Build"/> instances
    /// </summary>
    public class BuildCollection : List<Build>
    {
        internal static BuildCollection FromJson(JArray jArray)
        {
            var result = new BuildCollection();
            result.AddRange(jArray.Select(ResponseBase.FromJson<Build>));

            return result;
        }
    }
}