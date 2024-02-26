using Cinephile.Infrastructure.Repositories.Rest.Dtos.Movies;
using Refit;

namespace Cinephile.Infrastructure;

[Headers("Content-Type: application/json")]
public interface IRestClientContract
{
    /// <summary>
    /// Fetches upcoming movie data from the web service.
    /// </summary>
    /// <param name="apiKey">Our web api key expected by the web service.</param>
    /// <param name="page">Our page index.</param>
    /// <param name="language">The language we want to retrieve the details in.</param>
    /// <returns>An observable which signals with movie data transfer object.</returns>
    [Get("/movie/upcoming?api_key={apiKey}&language={language}&page={page}&sort_by=release_date")]
    IObservable<MovieDto> FetchUpcomingMovies(string apiKey, int page, string language);

    /// <summary>
    /// Fetches the image configuration from the service.
    /// </summary>
    /// <param name="apiKey">Our web api key expected by the web service.</param>
    /// <returns>An observable which signals with the image configuration.</returns>
    // [Get("/configuration?api_key={apiKey}")]
    // IObservable<ImageConfigurationDto> FetchImageConfiguration(string apiKey);

    /// <summary>
    /// Fetches the genres available.
    /// </summary>
    /// <param name="apiKey">Our web api key expected by the web service.</param>
    /// <param name="language">The language we want the genres for.</param>
    /// <returns>An observable which signals with the genre.</returns>
    [Get("/genre/movie/list?api_key={apiKey}&language={language}")]
    IObservable<MovieDetailsDto> FetchGenres(string apiKey, string language);
}
