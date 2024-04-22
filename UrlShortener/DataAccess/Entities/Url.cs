namespace UrlShortener.DataAccess.Entities;

public class Url
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortUrl { get; set; }
    public string OriginalUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}