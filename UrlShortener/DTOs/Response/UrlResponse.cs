namespace UrlShortener.DTOs.Response;

public class UrlResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortUrl { get; set; }
    public string OriginalUrl { get; set; }
    public string OwnerName { get; set; }
    public DateTime CreatedAt { get; set; }
}