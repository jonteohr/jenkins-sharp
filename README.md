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