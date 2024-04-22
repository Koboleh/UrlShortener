using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess.DbContexts;
using UrlShortener.DataAccess.Entities;
using UrlShortener.Interfaces;

namespace UrlShortener.DataAccess.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly UrlShortenerDbContext _dbContext;

    public UrlRepository(UrlShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Url?> GetUrlByIdAsync(int id)
    {
        return await _dbContext.Urls.FindAsync(id);
    }
    
    public async Task<Url?> GetUrlByShortUrlAsync(string shortUrl)
    {
        return await _dbContext.Urls.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
    }

    public IQueryable<Url> GetUrlsAsync()
    {
        return _dbContext.Urls
            .Include(u => u.User)
            .AsQueryable();
    }

    public IQueryable<Url> GetUserUrlsAsync(int userId)
    {
        return _dbContext.Urls
            .Include(u => u.User)
            .Where(u => u.UserId == userId)
            .AsQueryable();
    }

    public async Task CreateUrlAsync(Url url)
    {
        await _dbContext.AddAsync(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUrlAsync(Url url)
    {
        _dbContext.Update(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUrlAsync(Url url)
    {
        _dbContext.Remove(url);
        await _dbContext.SaveChangesAsync();
    }
    
}