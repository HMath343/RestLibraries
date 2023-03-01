``` ini

BenchmarkDotNet=v0.13.4, OS=Windows 10 (10.0.19045.2604)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.405
  [Host]     : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.13 (6.0.1322.58009), X64 RyuJIT AVX2


```
|                       Method |             Mean |          Error |         StdDev |   Gen0 |   Gen1 |   Gen2 | Allocated |
|----------------------------- |-----------------:|---------------:|---------------:|-------:|-------:|-------:|----------:|
| RestsharpClientInstanciation |         1.761 μs |      0.0184 μs |      0.0163 μs | 0.3071 | 0.0057 | 0.0019 |    3865 B |
|     RefitClientInstanciation |        21.109 μs |      0.2267 μs |      0.2010 μs | 1.1292 | 0.0305 |      - |   14497 B |
|            RestsharpGetFilms |               NA |             NA |             NA |      - |      - |      - |         - |
|                RefitGetFilms |               NA |             NA |             NA |      - |      - |      - |         - |
|         RestsharpGetFilmById | 4,074,903.100 μs | 19,438.7016 μs | 18,182.9735 μs |      - |      - |      - |   46640 B |
|             RefitGetFilmById |               NA |             NA |             NA |      - |      - |      - |         - |

Benchmarks with issues:
  BenchmarkWrapper.RestsharpGetFilms: DefaultJob
  BenchmarkWrapper.RefitGetFilms: DefaultJob
  BenchmarkWrapper.RefitGetFilmById: DefaultJob
