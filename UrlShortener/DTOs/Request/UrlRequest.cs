namespace UrlShortener.DTOs.Request;

public class UrlRequest
{
    public string Name { get; set; }
    public string OriginalUrl { get; set; }
    public int UserId { get; set; }
}