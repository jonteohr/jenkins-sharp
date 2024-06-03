using System;
using jenkins_api_cs;

namespace Example
{
    internal class Program
    {
        private readonly JenkinsClient _client;
        private const string JenkinsUrl = "https://jenkins.jonteohr.xyz/";
        
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            _client = new JenkinsClientBuilder()
                .SetUrl(JenkinsUrl)
                .Build();

            FetchJobInfo();
            Console.ReadKey();
        }

        private async void FetchJobInfo()
        {
            var myJob = await _client.GetJobInfoAsync("JENKINS.SHARP_TAGS");
            
            Console.WriteLine($"Job {myJob.Name} is currently {myJob.Color}!");
            Console.WriteLine($"Most recent build was #{myJob.LastBuild.Number}");
            Console.WriteLine($"Last successful run was #{myJob.LastSuccessfulBuild.Number}");
            
            if(myJob.LastFailedBuild != null) // In case there's a logged "last failed build"
                Console.WriteLine($"Last failed build was {myJob.LastFailedBuild.Number}");
        }
    }
}
