using Cinephile.Core;
using Cinephile.Core.Models;
using Cinephile.Infrastructure.Framework.Scheduler;
using Cinephile.Infrastructure.Repositories;
using Cinephile.Services;
using Cinephile.ViewModels.Services;
using Cinephile.ViewModels.ViewModels;
using Cinephile.Views;
using Microsoft.Extensions.Logging;

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
                    .AddSingleton<IMovieService, MovieService>() //TODO: Testing how this would work
                    .AddTransient<INavigationService, NavigationService>()
                    .AddSingleton<ISchedulerService, SchedulerService>()
                    .AddTransient<UpcomingMoviesListViewModel>()
                    .AddTransient<MovieDetailViewModel>()
                    .AddTransient<UpcomingMoviesListView>()
                    .AddTransient<UpcomingMoviesCellView>()
                    .AddTransient<UpcomingMoviesCellViewModel>()
                    .AddTransient<MovieDetailView>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
