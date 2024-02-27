namespace Cinephile.Core.Models;

public class Movie : IEquatable<Movie>
{
    public Movie(string id, string title, string overview, string posterSmall, string posterLarge, DateTime releaseDate, IList<string> genres)
    {
        Id = id;
        Title = title;
        Overview = overview;
        PosterSmall = posterSmall;
        PosterLarge = posterLarge;
        ReleaseDate = releaseDate;
        Genres = genres;
    }

    public Movie() { }

    public string Id { get; set; }
    public string Title { get; set; }
    public string Overview { get; set; }
    public string PosterSmall { get; set; }
    public string PosterLarge { get; set; }
    public DateTime ReleaseDate { get; set; }
    public IList<string> Genres { get; set; } = new List<string>();

    public bool Equals(Movie? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return Id == other.Id
            && Title == other.Title
            && Overview == other.Overview
            && PosterSmall == other.PosterSmall
            && PosterLarge == other.PosterLarge
            && ReleaseDate.Equals(other.ReleaseDate);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != this.GetType()) return false;

        return Equals((Movie)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, Overview, PosterSmall, PosterLarge, ReleaseDate);
    }
}
