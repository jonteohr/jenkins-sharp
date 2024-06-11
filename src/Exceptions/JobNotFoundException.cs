using System;

namespace jenkins_api_cs.Exceptions
{
    /// <summary>
    /// Exception thrown when a job is requested but not found
    /// </summary>
    public class JobNotFoundException : Exception
    {
        /// <summary>
        /// Main exception constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public JobNotFoundException(string message, Exception inner) : base(message,inner) {}
    }
}