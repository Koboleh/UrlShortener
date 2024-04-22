using UrlShortener.DataAccess.Entities;

namespace UrlShortener.Interfaces;

public interface IUrlRepository
{
    Task<Url?> GetUrlByIdAsync(int id);
    IQueryable<Url> GetUrlsAsync();
    IQueryable<Url> GetUserUrlsAsync(int userId);
    Task CreateUrlAsync(Url url);
    Task UpdateUrlAsync(Url url);
    Task DeleteUrlAsync(Url url);
}