using Cine.Core.Models;

namespace Cine.Core;

public interface IApiService
{
    IObservable<Movie> FetchUpcomingMovies(int page, string language);
}
