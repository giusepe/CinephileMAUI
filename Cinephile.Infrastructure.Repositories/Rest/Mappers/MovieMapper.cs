using System.Globalization;
using Cinephile.Core.Models;
using Cinephile.Infrastructure.Repositories.Rest.Dtos.Movies;

namespace Cinephile.Infrastructure;

/// <summary>
/// Maps the data transfer objects (DTO) to Movie instances.
/// </summary>
public static class MovieMapper
{
    private const string BaseUrl = "https://image.tmdb.org/t/p/";
    private const string SmallPosterSize = "w185";
    private const string BigPosterSize = "w500";

    /// <summary>
    /// Converts the DTO to their movie instances.
    /// </summary>
    /// <param name="genres">Gets the available movie genres.</param>
    /// <param name="movieDto">Gets the movie DTO instances.</param>
    /// <param name="language">Gets the language.</param>
    /// <returns>The mapped Movie instance.</returns>
    public static Movie ToModel(GenresDto genres, MovieResult movieDto, string language)
    {
        Console.WriteLine(string
                .Concat(
                    BaseUrl,
                    SmallPosterSize,
                    movieDto.PosterPath));
        return new Movie
        {
            Id = movieDto.Id.ToString(),
            Title = movieDto.Title,
            PosterSmall = string
                .Concat(
                    BaseUrl,
                    SmallPosterSize,
                    movieDto.PosterPath),
            PosterLarge = string
                .Concat(
                    BaseUrl,
                    BigPosterSize,
                    movieDto.PosterPath),
            Genres = genres.Genres.Where(g => movieDto.GenreIds.Contains(g.Id)).Select(j => j.Name).ToList(),
            ReleaseDate = DateTime.Parse(movieDto.ReleaseDate, new CultureInfo(language)),
            Overview = movieDto.Overview
        };
    }
}