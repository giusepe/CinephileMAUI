using System.Reactive.Linq;
using Cinephile.Core;
using Cinephile.Core.Models;
using Cinephile.Infrastructure.Repositories.Rest.Dtos.Movies;
using Refit;

namespace Cinephile.Infrastructure;

public class RestApiService : IApiService
{
    private const string ApiKey = "1f54bd990f1cdfb230adb312546d765d";

    private readonly IRestClientContract RestApi;
    public RestApiService()
    {
        RestApi = RestService.For<IRestClientContract>("https://api.themoviedb.org/3");
    }
    public int PageSize { get; } = 20;

    public IObservable<Movie> FetchMovie(string id, string language)
    {
        return RestApi
            .FetchMovie(id, language)
            .Select(movieDto => MovieMapper.ToModel(movieDto));
    }

    public IObservable<IEnumerable<Movie>> FetchUpcomingMovies(int index, string language)
    {
        int page = (int)Math.Ceiling(index / (double)PageSize) + 1;

        var result = RestApi
                .FetchUpcomingMovies(ApiKey, page, language)
                .CombineLatest(
                    RestApi.FetchGenres(ApiKey, language),
                    (movies, genres) =>
                    {
                        return movies
                            .Results
                            .Select(movieDto => MovieMapper.ToModel(genres, movieDto, language));
                    });

        return result;
    }
}
