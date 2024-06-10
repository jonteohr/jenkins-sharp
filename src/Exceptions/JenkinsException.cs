using System;

namespace jenkins_api_cs.Exceptions
{
    /// <summary>
    /// Exception thrown during Jenkins API calls
    /// </summary>
    public class JenkinsException : Exception
    {
        /// <summary>
        /// Main constructor of the exception
        /// </summary>
        /// <param name="message">A message explaining the issue</param>
        /// <param name="inner">The inner exception that caused this throw</param>
        public JenkinsException(string message, Exception inner) : base(message, inner)
        {}
    }
}