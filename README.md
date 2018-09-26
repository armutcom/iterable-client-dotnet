# iterable-client-dotnet - iterable.com Client Library for .Net

iterable-client-dotnet is a client library targeting .NET Standard 1.3, .NET Standard 2.0, .NET 4.5 and .NET 4.6.1 that provides an easy way to interact with [iterable.com API](https://www.iterable.com)

All API requests must be accompanied by a api key. You need to register then create an api key from [iterable.com Integrations](https://app.iterable.com/settings/apiKeys)

Because of armut.com is already using iterable, armut.com will keep the Armut.Iterable.Client and Armut.Iterable.Client.Extension project up to date and maintain it.

## Supported Platforms

* .NET 4.5 (Desktop / Server)
* .NET 4.6.1 (Desktop / Server)
* [.NET Standard 1.3](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
* [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

## Features
* Dependency injection friendly (can also be used standalone, see below)
* Supports async and sync calls.

## Continuous integration

| Build server                | Platform      | Build status                                                                                                                                                        | 
|-----------------------------|---------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Travis                      | Linux / MacOS | [![Build Status](https://travis-ci.org/armutcom/iterable-client-dotnet.svg?branch=master)](https://travis-ci.org/armutcom/iterable-client-dotnet)  | |
| Azure Pipelines             | Ubuntu        | [![Build status](https://dev.azure.com/armutcom/iterable-client-dotnet/_apis/build/status/iterable-client-dotnet%20-%20Ubuntu)](https://dev.azure.com/armutcom/iterable-client-dotnet/_build/latest?definitionId=3) | |

## Table of Contents

1. [Installation](https://github.com/armutcom/iterable-client-dotnet#installation)
2. [Usage](https://github.com/armutcom/iterable-client-dotnet#usage)
    - [Standalone Initialization](https://github.com/armutcom/iterable-client-dotnet#standalone-initialization)
    - [Microsoft.Extensions.DependencyInjection Initialization](https://github.com/armutcom/iterable-client-dotnet#microsoftextensionsdependencyinjection-initialization)
    - [Call Endpoints](https://github.com/armutcom/iterable-client-dotnet#call-endpoints)
    - [Synchronous Wrapper](https://github.com/armutcom/iterable-client-dotnet#synchronous-wrapper)
3. [Samples](https://github.com/armutcom/iterable-client-dotnet#samples)
4. [License](https://github.com/armutcom/iterable-client-dotnet#license)

## Installation

|       | Package |
|---------------|----------|
| iterable-client-dotnet | [![NuGet](https://img.shields.io/nuget/v/Armut.Iterable.Client.svg)](https://www.nuget.org/packages/Armut.Iterable.Client)    |
| iterable-client-extension-dotnet | [![NuGet](https://img.shields.io/nuget/v/Armut.Iterable.Client.Extension.svg)](https://www.nuget.org/packages/Armut.Iterable.Client.Extension)    |

Following commands can be used to install both Armut.Iterable.Client and Armut.Iterable.Client.Extension, run the following command in the Package Manager Console

```
Install-Package Armut.Iterable.Client
Install-Package Armut.Iterable.Client.Extension
```

Or use `dotnet cli`

```
dotnet Armut.Iterable.Client
dotnet Armut.Iterable.Client.Extension
```

## Usage

Armut.Iterable.Client can be used with any DI library, or it can be used standalone.

### Standalone Initialization

If you do not want to use any DI framework, you have to instantiate `IterableStandalone` as follows.

#### IterableStandalone
```csharp
IIterableFactory iterableFactory = IterableStandalone.Create("your_api_key");
UserClient client = iterableFactory.CreateUserClient();
```

`IIterableFactory` contains all necessary clients.

### Microsoft.Extensions.DependencyInjection Initialization

First, you need to install `Microsoft.Extensions.DependencyInjection` and `Microsoft.Extensions.Http` NuGet package as follows

```
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Http
```

By installing `Microsoft.Extensions.Http` you will be able to use [`HttpClientFactory`](https://www.stevejgordon.co.uk/introduction-to-httpclientfactory-aspnetcore).In the words of the ASP.NET Team it is “an opinionated factory for creating HttpClient instances” and is a new feature comes with the release of ASP.NET Core 2.1. 

If you don't want to use `HttpClientFactory`, you must register `HttpClient` yourself with the container or you can use a factory with yout DI framework as follows
```csharp
var serviceCollection = new ServiceCollection();

HttpClient iterableHttpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.iterable.com/")
};

iterableHttpClient.DefaultRequestHeaders.Add("Api-Key", "your_api_key");

serviceCollection.AddSingleton(clientFactory =>
{
    return (Func<string, HttpClient>)(key =>
    {
        switch (key)
        {
            case "IterableClient":
                return iterableHttpClient;
            default:
                return null;
        }
    });
});

var serviceProvider = services.BuildServiceProvider();
var userClient = serviceProvider.GetRequiredService<IUserClient>();
```

By referencing Armut.Iterable.Client.Extension, register necessary dependencies to `ServiceCollection` as follows
```csharp
var serviceCollection = new ServiceCollection();
serviceCollection.AddIterableClient("your_api_key");

var serviceProvider = services.BuildServiceProvider();
var userClient = serviceProvider.GetRequiredService<IUserClient>();
```

or
```csharp
var serviceCollection = new ServiceCollection();
HttpClient iterableHttpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.iterable.com/")
};

iterableHttpClient.DefaultRequestHeaders.Add("Api-Key", "your_api_key");

serviceCollection.AddSingleton(clientFactory =>
{
    return (Func<string, HttpClient>)(key =>
    {
        switch (key)
        {
            case "IterableClient":
                return iterableHttpClient;
            default:
                return null;
        }
    });
});
serviceCollection.AddIterableClient();

var serviceProvider = services.BuildServiceProvider();
var userClient = serviceProvider.GetRequiredService<IUserClient>();
```

### Call Endpoints

The methods that end with Async returns model itself without additional HTTP response information.

```csharp
UserModel userModel = await userClient.GetByEmailAsync("info@armut.com");
```

### Synchronous Wrapper

For synchronous calls, .NET Framework's .GetAwaiter().GetResult() method can be called.

```csharp
UserModel userModel = userClient.GetByEmailAsync("info@armut.com").GetAwaiter().GetResult();
```

### Samples

You can find all of the samples from [here](sample)

## License
Licensed under MIT, see [LICENSE](LICENSE) for the full text.