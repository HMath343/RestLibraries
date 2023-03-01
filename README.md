# Benchmark.DotNet.RestLibraries

##  Goal

This project is aimed to compare performance between Restfit & Restsharp library by consuming a local API.


*HTTPClient hasn't been considered as boiler plate will outweight benefit of using Refit/Restsharp*

## Result

### Config 
``` 
BenchmarkDotNet=v0.13.4, OS=Windows 10 (10.0.19045.2604)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.405
  [Host]     : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
```

### Run 1 
```
|                       Method |       Mean |      Error |     StdDev |     Median |    Gen0 |    Gen1 |   Gen2 | Allocated |
|----------------------------- |-----------:|-----------:|-----------:|-----------:|--------:|--------:|-------:|----------:|
| RestsharpClientInstanciation |   1.786 μs |  0.0340 μs |  0.0443 μs |   1.800 μs |  0.3071 |  0.0057 | 0.0019 |   3.77 KB |
|     RefitClientInstanciation |  20.749 μs |  0.1702 μs |  0.1592 μs |  20.653 μs |  1.1292 |  0.0305 |      - |  14.15 KB |
|            RestsharpGetFilms | 950.939 μs | 34.9955 μs | 97.5534 μs | 918.690 μs | 28.3203 | 10.7422 |      - | 346.89 KB |
|                RefitGetFilms | 804.518 μs | 27.2178 μs | 74.9657 μs | 772.008 μs |  3.9063 |  0.9766 |      - |  58.54 KB |
|         RestsharpGetFilmById | 549.706 μs | 10.1179 μs | 25.7533 μs | 538.561 μs |  4.8828 |       - |      - |  62.34 KB |
|             RefitGetFilmById | 516.559 μs |  4.9200 μs |  4.6022 μs | 516.296 μs |  0.9766 |       - |      - |  12.15 KB |
```
 
### Run 2
```
|                       Method |       Mean |     Error |    StdDev |    Gen0 |    Gen1 |   Gen2 | Allocated |
|----------------------------- |-----------:|----------:|----------:|--------:|--------:|-------:|----------:|
| RestsharpClientInstanciation |   1.752 us | 0.0197 us | 0.0184 us |  0.3071 |  0.0057 | 0.0019 |   3.77 KB |
|     RefitClientInstanciation |  20.407 us | 0.1474 us | 0.1307 us |  1.1292 |  0.0305 |      - |  14.15 KB |
|            RestsharpGetFilms | 864.620 us | 7.0563 us | 5.8923 us | 28.3203 | 10.7422 |      - | 346.97 KB |
|                RefitGetFilms | 756.474 us | 8.4349 us | 7.4773 us |  3.9063 |       - |      - |  58.87 KB |
|         RestsharpGetFilmById | 542.903 us | 5.9873 us | 5.6005 us |  4.8828 |       - |      - |  62.39 KB |
|             RefitGetFilmById | 518.222 us | 3.8545 us | 3.2187 us |  0.9766 |       - |      - |  12.15 KB |
```

### Run 3
```
|                       Method |       Mean |      Error |     StdDev |     Median |    Gen0 |    Gen1 |   Gen2 | Allocated |
|----------------------------- |-----------:|-----------:|-----------:|-----------:|--------:|--------:|-------:|----------:|
| RestsharpClientInstanciation |   1.846 us |  0.0352 us |  0.0419 us |   1.855 us |  0.3071 |  0.0057 | 0.0019 |   3.77 KB |
|     RefitClientInstanciation |  20.780 us |  0.4135 us |  0.4762 us |  20.704 us |  1.1292 |  0.0305 |      - |  14.15 KB |
|            RestsharpGetFilms | 853.993 us | 14.5339 us | 12.1364 us | 853.673 us | 28.3203 | 10.7422 |      - | 346.97 KB |
|                RefitGetFilms | 748.744 us |  6.5500 us |  5.8064 us | 747.344 us |  3.9063 |  0.9766 |      - |  58.83 KB |
|         RestsharpGetFilmById | 545.835 us |  5.1056 us |  4.7758 us | 546.611 us |  4.8828 |       - |      - |  62.33 KB |
|             RefitGetFilmById | 572.614 us | 20.0200 us | 59.0293 us | 540.195 us |  0.9766 |       - |      - |  12.14 KB |
``` 

## Description 

### Benchmark.RestLibraries.API 

API project using minimal API. 


*Boiler plate as been limited to have the fatest response as we're trying to mesure Refit/Resharp consuming this API.*

### Benchmark.RestLibraries.Core 

Contain classes to run Benchmark

## Resource 

- Used JSON payload [Star wars public API](https://swapi.dev/) 
- Used model class [Star wars SharpTrooper](https://github.com/olcay/SharpTrooper)

## Running benchmark

    - docker build -t benchmark-restlibraries-api -f Dockerfile .
    - docker run -d -p 80:80 benchmark-restlibraries-api
    - dotnet run -p BenchMark.DotNet.RestLibraries.Core.csproj -c Release
