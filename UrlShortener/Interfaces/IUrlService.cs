using UrlShortener.DTOs.Request;
using UrlShortener.DTOs.Response;

namespace UrlShortener.Interfaces;

public interface IUrlService
{
    Task<PaginationResponse<UrlListResponse>> GetUrlsAsync(PaginationRequest request);
    Task<string> GetUrlByShortUrlValueAsync(string shortUrlValue);
    Task<UrlResponse> GetUrlByIdAsync(int id);
    Task CreateUrlAsync(UrlRequest request);
    Task DeleteUrlByIdAsync(int id);
    Task<bool> IsUrlOwnerAsync(int urlId, int userId);
}