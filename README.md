# RestLibraries 

A project aimed to compare different .NET API client libraries.

Use case :
- A user trying to save a payment in a different currency
- An external call to an API giving back rates between two currency
    - For benchmark test purpose, it has been hardcoded with the payload of [Free forex API](https://www.freeforexapi.com/api/live?pairs=EURGBP)

## Project

Simple API endpoint taking payment data and using different library to call a external API. 
- *No persistence as it's aimed to measure library performance*

### RestLibraries.API

Minimal API aimed to avoid rate limiting and network latency for Benchmark performances

docker build -t restlibraries-api .
docker run -d -p 80:80 --name restlibraries-api restlibraries-api

### RestLibraries 

Project respecting Clean Architecture & using one endpoint / handler doing the same process for each library.
- Take a payment payload
- If currency is different, makes an external API call to get the rates between currency
- Return payment result

- An post payment endpoint per API client library :
    - [HttpClient](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines)
    - [Refit](https://github.com/reactiveui/refit)
    - [Restsharp](https://github.com/restsharp/RestSharp)

## Benchmark

### Running the bench mark

- cd .\RestLibraries\test\BenchMarkTests
- dotnet test --filter "Category=HttpClient" -c Release
- dotnet test --filter "Category=Refit" -c Release
- dotnet test --filter "Category=Restsharp" -c Release

### Issue & note

Benchmark aren't running smoothly due to .NetCore HttpClient connection pool management (build with HttpClientFactory under the hood).
It's still a bit blurry and I may have made a rookie mistake somewhere.
[Dotnet runtime github issue](https://github.com/dotnet/runtime/issues/43764)

```
   at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.System.Threading.Tasks.Sources.IValueTaskSource<System.Int32>.GetResult(Int16 token)
   at System.Net.Http.HttpConnection.InitialFillAsync(Boolean async)
   at System.Net.Http.HttpConnection.SendAsyncCore(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
```

I've tried a few things without luck :

- Make sure HttpClient was implemented as a singleton to make sure that it's not disposed
- Playing with MaxConnectionsPerServer settings for HttpClient

```
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new SocketsHttpHandler()
        {
            MaxConnectionsPerServer = 10
        };
    })
```

### Result 

1. HttpClient

```
BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2728/22H2/2022Update)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.405
  [Host]   : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
  ShortRun : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  
```

|               Method |     Mean |    Error |    StdDev |   Gen0 |   Gen1 | Allocated |
|--------------------- |---------:|---------:|----------:|-------:|-------:|----------:|
| BenchMark_HttpClient | 8.763 μs | 4.231 μs | 0.2319 μs | 0.2289 | 0.1221 |   3.18 KB |

Updated (With System.Net.Http.Json deserialisation)

|               Method |     Mean |     Error |   StdDev |   Gen0 |   Gen1 | Allocated |
|--------------------- |---------:|----------:|---------:|-------:|-------:|----------:|
| BenchMark_HttpClient | 7.752 μs | 42.137 μs | 2.310 μs | 0.3510 | 0.1907 |   4.28 KB |

2. Refit

```
BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2728/22H2/2022Update)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.405
  [Host]   : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
  ShortRun : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  
```

|          Method |     Mean |    Error |   StdDev |   Gen0 |   Gen1 | Allocated |
|---------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| BenchMark_Refit | 25.26 μs | 28.64 μs | 1.570 μs | 0.7324 | 0.0610 |   9.11 KB |

3. Restsharp

*Only library to throw socket exception with Job.ShortRun*

```
BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2728/22H2/2022Update)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.405
  [Host]   : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
  ShortRun : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  
```
|              Method |     Mean |     Error |   StdDev |   Gen0 |   Gen1 | Allocated |
|-------------------- |---------:|----------:|---------:|-------:|-------:|----------:|
| BenchMark_Restsharp | 82.16 μs | 238.85 μs | 13.09 μs | 2.4414 | 0.7324 |  26.81 KB |


With singleton

```
|              Method |     Mean |    Error |   StdDev |   Gen0 |   Gen1 | Allocated |
|-------------------- |---------:|---------:|---------:|-------:|-------:|----------:|
| BenchMark_Restsharp | 116.4 μs | 352.7 μs | 19.33 μs | 2.4414 | 0.9766 |  29.62 KB |
```

### Wrap-up

As all software engineering question, best answer would be **it depends**.

1. HttpClient 
    - It will induce a bit more boiler plate due to HttpRequest exception handling
    - Best performance usage for API endpoint needing an high throughput
        - I'd consider this if DB access isn't a bottleneck & endpoint are called a lot
    - Easy to make mistakes with disposable objects (stream, ...)
2. Refit
    - Library with the least amount of boilerplate
    - Performance isn't that far for HttpClient (need to confirm with huge json payload)
    - Library hasn't been updated since 08/02/2022
3. Restsharp 
    - Less boilerplate than HttpClient but more than Refit
    - Benchmark looks to have too much error to be reliable except memory allocation
    - Active development

Further reading :
- https://github.com/joseftw/jos.httpclient
- https://www.stevejgordon.co.uk/httpclient-connection-pooling-in-dotnet-core
- https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/

- https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json