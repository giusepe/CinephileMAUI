using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Cine.Core;
using Cine.Core.Models;
using DynamicData;
using Moq;

namespace Cire.Core.Tests;

[TestFixture]
public class MovieServiceTest
{
    private IList<Movie> _movies = new List<Movie>();

    [SetUp]
    public void Setup()
    {
        var releaseDate = DateTime.Now;
        for (int i = 0; i < 20; i++)
        {
            var movie = new Movie()
            {
                Id = i.ToString(),
                Title = $"Test Movie {i}",
                Overview = $"Test Description {i}",
                ReleaseDate = releaseDate,
                Genres = ["Action"],
            };

            _movies.Add(movie);
        }
    }

    [Test]
    public void GetUpcomingMovies_Zero_20Movies()
    {
        var apiServiceMock = new Mock<IApiService>();

        apiServiceMock
            .Setup(x => x.FetchUpcomingMovies(It.IsAny<int>(), It.IsAny<string>()))
            .Returns(_movies.ToObservable());

        var target = new MovieService(apiServiceMock.Object);

        target.Movies.Connect().Bind(out ReadOnlyObservableCollection<Movie> actual).Subscribe();

        target.LoadUpcomingMovies(0).Subscribe();

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Count(), Is.EqualTo(20));
    }
}
