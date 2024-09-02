using System;
using jenkins_api_cs;
using jenkins_api_cs.Authentication;
using jenkins_api_cs.Exceptions;

namespace Example
{
    internal class Program
    {
        private readonly JenkinsClient m_client;
        private readonly JenkinsCredentials m_credentials;
        private const string JenkinsUrl = "https://jenkins.jonteohr.xyz";
        
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            try
            {
                m_credentials = new JenkinsCredentials("username", "apiKey");

                m_client = new JenkinsClientBuilder()
                    .SetUrl(JenkinsUrl)
                    .WithCredentials(m_credentials) // OPTIONAL: Sets the credentials to use when sending requests to the Jenkins instance.
                    .Build();

                FetchJobInfo();

                Console.ReadKey();
            }
            catch (JenkinsException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void FetchJobInfo()
        {
            // Fetch data on a specific job
            var myJob = await m_client.GetJobInfoAsync("JENKINS.SHARP_TAGS");
            
            // Print out some data on the requested job
            Console.WriteLine($"Job {myJob.FullName} is currently {myJob.Color}!");
            Console.WriteLine($"Most recent build was #{myJob.LastBuild.Number}");
            Console.WriteLine($"Last successful run was #{myJob.LastSuccessfulBuild.Number}");
            Console.WriteLine($"This job is of type: {myJob.JobType}");
            
            // For all builds still on record, print their number
            myJob.Builds.ForEach(build => Console.WriteLine($"Previous builds: #{build.Number}"));

            Console.WriteLine($"Next build number is: #{myJob.NextBuildNumber}");

            // Get more data on the most recent build from said job
            var lastBuild = await m_client.GetBuildInfoAsync(myJob.Name, myJob.LastBuild.Number);
            Console.WriteLine($"Last build was: {lastBuild.Result}"); // Result from the last job
            
            if(myJob.LastFailedBuild != null) // In case there's a logged "last failed build"
                Console.WriteLine($"Last failed build was {myJob.LastFailedBuild.Number}");
        }
    }
}
