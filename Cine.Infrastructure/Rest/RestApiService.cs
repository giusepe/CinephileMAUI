using System.Reactive.Linq;
using Cine.Core;
using Cine.Core.Models;
using Refit;

namespace Cine.Infrastructure;

public class RestApiService : IApiService
{
    private const string ApiKey = "1f54bd990f1cdfb230adb312546d765d";
    // public RestApiService()
    // {
    //     IRestClientContract CreateClient(HttpMessageHandler messageHandler)
    //     {
    //         var client = new HttpClient(messageHandler)
    //         {
    //             BaseAddress = new Uri(apiBaseAddress ?? ApiBaseAddress)
    //         };

    //         return RestService.For<IRestApiClient>(client);
    //     }
    // }
    public int PageSize { get; } = 20;

    public IObservable<Movie> FetchUpcomingMovies(int index, string language)
    {
        var restApi = RestService.For<IRestClientContract>("https://api.themoviedb.org/3");

        int page = (int)Math.Ceiling(index / (double)PageSize) + 1;

        return restApi
                .FetchUpcomingMovies(ApiKey, page, language)
                .CombineLatest(
                    restApi.FetchGenres(ApiKey, language),
                    (movies, genres) =>
                    {
                        return movies
                            .Results
                            .Select(movieDto => MovieMapper.ToModel(genres, movieDto, language));
                    })
                    .SelectMany(x => x);
    }
}
