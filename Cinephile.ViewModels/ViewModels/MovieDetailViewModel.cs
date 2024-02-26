// Copyright (c) 2019 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Globalization;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Cinephile.Core.Models;
using Cinephile.Infrastructure.Framework.Scheduler;
using ReactiveUI;

namespace Cinephile.ViewModels.ViewModels
{
    /// <summary>
    /// A view model containing details about a movie.
    /// </summary>
    public class MovieDetailViewModel : ViewModelBase
    {
        private readonly ObservableAsPropertyHelper<Movie> _movie;

        public Movie Movie => _movie.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieDetailViewModel"/> class.
        /// </summary>
        public MovieDetailViewModel(ISchedulerService schedulerService, IMovieService movieService)
            : base(schedulerService)
        {
            this.WhenAnyValue(x => x.MovieId)
                .Do(x => Console.WriteLine($"Value: {x}"))
                .WhereNotNull()
                .Select(id => movieService.GetMovie(id))
                .Switch()
                .ToProperty(this, nameof(Movie), out _movie, scheduler: SchedulerService.MainThreadScheduler);
        }

        /// <summary>
        /// Gets the title of the movie.
        /// </summary>
        public string Title => Movie?.Title;

        /// <summary>
        /// Gets the URL to the small movie poster.
        /// </summary>
        public string PosterSmall => Movie?.PosterSmall;

        /// <summary>
        /// Gets the URL to the big movie poster.
        /// </summary>
        public string PosterBig => Movie?.PosterLarge;

        /// <summary>
        /// Gets the genres of the movie.
        /// </summary>
        public string Genres => string.Join(", ", Movie?.Genres);

        /// <summary>
        /// Gets the release date of the movie.
        /// </summary>
        public string ReleaseDate => Movie?.ReleaseDate.ToString("D", CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets an overview of the movie.
        /// </summary>
        public string Overview => Movie?.Overview;

        public string MovieId { get; set; }//  => _movieId.Value;
    }
}
