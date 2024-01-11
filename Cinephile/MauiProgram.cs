using Cinephile.Core;
using Cinephile.Core.Models;
using Cinephile.Infrastructure;
using Cinephile.ViewModels;
using Cinephile.Views;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace Cinephile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .Services
                    .AddTransient<IApiService, RestApiService>()
                    .AddTransient<IMovieService, MovieService>()
                    .AddTransient(x => new UpcomingMoviesListViewModel(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, x.GetService<IMovieService>()))
                    .AddTransient<UpcomingMoviesListView>()
                    .AddTransient<UpcomingMoviesCellView>()
                    .AddTransient<UpcomingMoviesCellViewModel>()
                    .AddTransient<MovieDetailView>()
                    .AddTransient<MovieDetailViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
