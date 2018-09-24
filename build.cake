#addin "nuget:?package=Cake.Incubator&version=3.0.0"

using System;
using System.Diagnostics;

var target = Argument("target", "Default");

Task("Default")
    .IsDependentOn("Build");

Task("Build")
    .Does(() =>
{
    DotNetCoreBuildSettings settings = new DotNetCoreBuildSettings();
    settings.NoRestore = true;
    settings.Configuration = "Release";

    var projects = GetFiles("src/**/*.csproj");

    Information($"Restoring projects");
    foreach(var project in projects)
    {
        DotNetCoreRestore(project.ToString());
    }

    Information($"Building projects");
    foreach(var project in projects)
    {
        DotNetCoreBuild(project.ToString(), settings);
    }
});

Task("Nuget-Pack")
    .Description("Publish to nuget")
    .Does(() =>
    {
        var settings = new DotNetCorePackSettings();
        settings.Configuration = "Release";

        settings.OutputDirectory = "./artifacts/Armut.Iterable.Client";
        settings.WorkingDirectory = "src/Client";
        DotNetCorePack("Client.csproj", settings);

        settings.OutputDirectory = "./artifacts/Armut.Iterable.Client.Extension";
        settings.WorkingDirectory = "src/Client.Extension";
        DotNetCorePack("Client.Extension.csproj", settings);
    });


RunTarget(target);