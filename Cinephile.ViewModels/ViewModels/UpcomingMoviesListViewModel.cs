using System;
using System.Reactive;
using System.Reactive.Concurrency;
using Cinephile.Core.Models;
using ReactiveUI;
using DynamicData;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using Cinephile.ViewModels.Services;
using System.Diagnostics;
using Cinephile.Infrastructure.Framework.Scheduler;

namespace Cinephile.ViewModels.ViewModels;

/// <summary>
/// A view model that contains a list of movies.
/// </summary>
public class UpcomingMoviesListViewModel : ViewModelBase
{
    private readonly ReadOnlyObservableCollection<UpcomingMoviesCellViewModel> _movies;
    private readonly ObservableAsPropertyHelper<bool> _isRefreshing;
    private UpcomingMoviesCellViewModel _selectedItem;
    private UpcomingMoviesCellViewModel _itemAppearing;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpcomingMoviesListViewModel"/> class.
    /// </summary>
    /// <param name="mainThreadScheduler">The scheduler to use for main thread operations.</param>
    /// <param name="taskPoolScheduler">The scheduler to use for task pool operations.</param>
    /// <param name="movieService">The service to use to retrieve movie information.</param>
    /// <param name="hostScreen">The screen to use for routing operations.</param>
    public UpcomingMoviesListViewModel(
            ISchedulerService schedulerService,
            INavigationService navigationService,
            IMovieService movieService)
        : base(schedulerService)
    {
        IMovieService movieService1 = movieService;

        LoadMovies = ReactiveCommand.CreateFromObservable<int, Unit>(count => movieService1.LoadUpcomingMovies(count));

        movieService1
            .UpcomingMovies
            .Connect()
            .SubscribeOn(SchedulerService.TaskPoolScheduler)
            .ObserveOn(SchedulerService.TaskPoolScheduler)
            .Transform(movie => new UpcomingMoviesCellViewModel(SchedulerService, movie))//, (o, n) => o = new UpcomingMoviesCellViewModel(n))
            .DisposeMany()
            .ObserveOn(SchedulerService.MainThreadScheduler)
            .Bind(out _movies)
            .Subscribe();

        LoadMovies.Subscribe();

        this
            .WhenAnyValue(x => x.SelectedItem)
            .Where(x => x != null)
            .Do(x => Debug.WriteLine($"Navigating {x.Movie.Id}"))
            .SelectMany(x => navigationService.GoTo($"movies/detail?movieId={x.Movie.Id}")
            
            //HostScreen
            //    .Router
            //    .Navigate
            //    .Execute(new MovieDetailViewModel(x.Movie, MainThreadScheduler, TaskPoolScheduler))
                )
            .Subscribe();

        LoadMovies
            .ThrownExceptions
            .ObserveOn(SchedulerService.MainThreadScheduler)
            .SelectMany(ex => ShowAlert.Handle(new AlertViewModel("Oops", ex.Message, "Ok")))
            .Subscribe();

        // TODO: Find out why ToProperty is at fault
        _isRefreshing =
            LoadMovies
                .IsExecuting
                .ToProperty(this, x => x.IsRefreshing, true, SchedulerService.MainThreadScheduler);

        this
            .WhenAnyValue(x => x.ItemAppearing)
            .Select(item =>
            {
                int offset = -1;

                var itemIndex = Movies.IndexOf(item);
                if (itemIndex == Movies.Count - 8)
                {
                    offset = Movies.Count;
                }

                return offset;
            })
            .Where(index => index > 0)
            .InvokeCommand(LoadMovies);
    }

    /// <summary>
    /// Gets a collection of movies.
    /// </summary>
    public ReadOnlyObservableCollection<UpcomingMoviesCellViewModel> Movies => _movies;

    /// <summary>
    /// Gets or sets the currently selected item.
    /// </summary>
    public UpcomingMoviesCellViewModel SelectedItem
    {
        get { return _selectedItem; }
        set { this.RaiseAndSetIfChanged(ref _selectedItem, value); }
    }

    /// <summary>
    /// Gets or sets items that are appearing.
    /// </summary>
    public UpcomingMoviesCellViewModel ItemAppearing
    {
        get { return _itemAppearing; }
        set { this.RaiseAndSetIfChanged(ref _itemAppearing, value); }
    }

    /// <summary>
    /// Gets a command which will load the movies at the specified page index.
    /// </summary>
    public ReactiveCommand<int, Unit> LoadMovies
    {
        get;
    }

    /// <summary>
    /// Gets a command which will open the about box.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> OpenAboutView
    {
        get;
    }

    /// <summary>
    /// Gets a value indicating whether we are refreshing the display.
    /// </summary>
    public bool IsRefreshing => _isRefreshing.Value;
}