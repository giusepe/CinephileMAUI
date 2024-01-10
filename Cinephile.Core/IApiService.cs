using Cinephile.Core.Models;

namespace Cinephile.Core;

public interface IApiService
{
    IObservable<IEnumerable<Movie>> FetchUpcomingMovies(int page, string language);
}
