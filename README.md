# jenkins-sharp [![](https://jenkins.jonteohr.xyz/view/NuGet/job/JENKINS.SHARP_TAGS/badge/icon)](https://jenkins.jonteohr.xyz/view/NuGet/job/JENKINS.SHARP_TAGS/)
Basic C# wrapper for using Jenkins HTTP/REST API.

## Setup
Add the api to your project from nuget

**Dotnet CLI**
```
dotnet add package jenkins.sharp
```
**PackagerManager**
```
NuGet\Install-Package jenkins.sharp
```

## Usage
You can find the [example project here](Example).

Create the `JenkinsClient` that you need to query build statuses:
```csharp
var client = new JenkinsClientBuilder()
    .SetUrl(YOUR_URL_TO_JENKINS)
    .Build();
```

Then to query a specific build:
```csharp
var MyJobName = "YOUR_JOB_NAME";

// Fetches information regarding a specific job and its recent status
var jobStatus = await client.GetJobInfoAsync(
    jobName: MyJobName
);
// Fetches information regarding a specific build inside a job
var buildStatus = await client.GetBuildInfoAsync(
    jobName: MyJobName,
    buildNo: jobStatus.LastSuccessfulBuild.Number
);

Console.WriteLine($"{jobStatus.FullName} is currently {jobStatus.Color}.");
Console.WriteLine($"Last successful is: #{jobStatus.LastSuccessfulBuild.Number}");
```

You can also iterate through all jobs on the server, or in a folder:
```csharp
var jobCollection = await client.GetAllJobsAsync(); // To fetch jobs inside a folder, use the overload GetAllJobs(string)

foreach(var job in jobCollection) {
    Console.WriteLine($"Name: {job.Name}");
    Console.WriteLine($"Url: {job.Url}");
    Console.WriteLine($"Job type: {job.JobType}");
}
```