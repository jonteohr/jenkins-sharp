namespace jenkins_api_cs.Authentication
{
    /// <summary>
    /// Main class for credentials provider
    /// </summary>
    public class JenkinsCredentials
    {
        /// <summary>
        /// The username associated with the API token
        /// </summary>
        public string Username { get; }
        /// <summary>
        /// The API token generated
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Main constructor for the credentials class
        /// </summary>
        /// <param name="username">The username associated with the API token</param>
        /// <param name="apiKey">The API token generated</param>
        public JenkinsCredentials(string username, string apiKey)
        {
            Username = username;
            ApiKey = apiKey;
        }
    }
}
