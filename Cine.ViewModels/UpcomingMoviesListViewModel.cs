using System.Reactive;
using System.Reactive.Concurrency;
using Cine.Core.Models;
using ReactiveUI;
using DynamicData;
using System.Reactive.Linq;
using System;
using System.Collections.ObjectModel;

namespace Cine.ViewModels;

public class UpcomingMoviesListViewModel : ViewModelBase
{
    public UpcomingMoviesListViewModel(IMovieService movieService, IScheduler? mainThreadScheduler, IScheduler? taskPoolScheduler)
        : base(mainThreadScheduler, taskPoolScheduler)
    {
        LoadMovies = ReactiveCommand.CreateFromObservable<int, Unit>(count => movieService.LoadUpcomingMovies(count));

        movieService
            .Movies
            .Connect()
            .SubscribeOn(TaskPoolScheduler)
            .ObserveOn(TaskPoolScheduler)
            .Transform(movie => new UpcomingMovieCellViewModel(movie, mainThreadScheduler, taskPoolScheduler))
            .DisposeMany()
            .ObserveOn(MainThreadScheduler)
            .Bind(out _movies)
            .Subscribe();

        LoadMovies.Subscribe();

        // LoadMovies
        //         .ThrownExceptions
        //         .ObserveOn(MainThreadScheduler)
        //         .SelectMany(ex => ShowAlert.Handle(new AlertViewModel("Oops", ex.Message, "Ok")))
        //         .Subscribe();
    }

    private readonly ObservableAsPropertyHelper<bool> _isRefreshing;

    public ReactiveCommand<int, Unit> LoadMovies
    {
        get;
    }

    public bool IsRefreshing => _isRefreshing.Value;
    private readonly ReadOnlyObservableCollection<UpcomingMovieCellViewModel> _movies;

    public ReadOnlyObservableCollection<UpcomingMovieCellViewModel> Movies => _movies;
}