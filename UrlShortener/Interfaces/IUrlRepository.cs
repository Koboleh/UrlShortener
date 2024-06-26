﻿using UrlShortener.DataAccess.Entities;

namespace UrlShortener.Interfaces;

public interface IUrlRepository
{
    Task<Url?> GetUrlByIdAsync(int id);
    Task<Url?> GetUrlByShortUrlAsync(string shortUrl);
    IQueryable<Url> GetUrlsAsync();
    Task<Url?> GetUrlByOriginalUrlAsync(string originalValue);
    Task CreateUrlAsync(Url url);
    Task UpdateUrlAsync(Url url);
    Task DeleteUrlAsync(Url url);
}