using System.Reactive;
using DynamicData;

namespace Cinephile.Core.Models;

public interface IMovieService
{
    IObservableCache<Movie, string> UpcomingMovies { get; }

    IObservable<Unit> LoadUpcomingMovies(int index);

    IObservable<Movie> GetMovie(string id);
}
