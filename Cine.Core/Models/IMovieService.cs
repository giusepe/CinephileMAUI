using System.Reactive;
using DynamicData;

namespace Cine.Core.Models;

public interface IMovieService
{
    IObservableCache<Movie, string> Movies { get; }

    IObservable<Unit> LoadUpcomingMovies(int index);
}
