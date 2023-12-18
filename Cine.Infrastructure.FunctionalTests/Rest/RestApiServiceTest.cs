using System.Reactive.Linq;
using Cine.Core.Models;

namespace Cine.Infrastructure.FunctionalTests.Rest;

[TestFixture]
public class RestApiServiceTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FetchUpcomingMovies_Zero_Results()
    {
        var target = new RestApiService();
        var actual = new List<Movie>();
        target.FetchUpcomingMovies(0, "en-US").Subscribe(actual.Add);

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Count(), Is.EqualTo(20));
    }
}