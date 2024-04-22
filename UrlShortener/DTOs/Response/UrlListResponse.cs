namespace UrlShortener.DTOs.Response;

public class UrlListResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortUrl { get; set; }
    public string OriginalUrl { get; set; }
}