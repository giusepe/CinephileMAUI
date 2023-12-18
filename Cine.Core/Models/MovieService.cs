using System.Reactive;
using System.Reactive.Linq;
using DynamicData;

namespace Cine.Core.Models;

public class MovieService : IMovieService
{
    public MovieService(IApiService apiService)
    {
        _apiService = apiService;
        _internalSourceCache = new SourceCache<Movie, string>(o => o.Id);
    }

    private string _language { get; } = "en-US";
    private readonly SourceCache<Movie, string> _internalSourceCache;
    public IObservableCache<Movie, string> Movies => _internalSourceCache;

    private IApiService _apiService { get; }

    public IObservable<Unit> LoadUpcomingMovies(int index)
    {
        return _apiService
            .FetchUpcomingMovies(index, _language)
            .Select(x =>
            {
                _internalSourceCache.Edit(innerCache => innerCache.AddOrUpdate(x));
                return Unit.Default;
            });
    }
}
