using System.Globalization;
using System.Reactive.Concurrency;
using Cinephile.Core.Models;

namespace Cinephile.ViewModels;

public class UpcomingMovieCellViewModel : ViewModelBase
{
    public UpcomingMovieCellViewModel(Movie movie, IScheduler? mainThreadScheduler, IScheduler? taskPoolScheduler)
        : base(mainThreadScheduler, taskPoolScheduler)
    {
        Movie = movie;
    }

    /// <summary>
    /// Gets the movie information.
    /// </summary>
    public Movie Movie { get; }

    /// <summary>
    /// Gets the title of the movie.
    /// </summary>
    public string Title => Movie.Title;

    /// <summary>
    /// Gets the path to the poster.
    /// </summary>
    public string PosterPath => Movie.PosterSmall;

    /// <summary>
    /// Gets the genres for the movie.
    /// </summary>
    public string Genres => string.Join(", ", Movie.Genres);

    /// <summary>
    /// Gets the release date of the movie.
    /// </summary>
    public string ReleaseDate => Movie.ReleaseDate.ToString("D", CultureInfo.CurrentCulture);
}