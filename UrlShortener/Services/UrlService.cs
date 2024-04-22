using AutoMapper;
using Gridify;
using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess.Entities;
using UrlShortener.DTOs.Request;
using UrlShortener.DTOs.Response;
using UrlShortener.Interfaces;

namespace UrlShortener.Services;

public class UrlService : IUrlService
{
    private readonly IMapper _mapper;
    private readonly IUrlRepository _urlRepository;

    public UrlService(IUrlRepository urlRepository, IMapper mapper)
    {
        _urlRepository = urlRepository;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<UrlListResponse>> GetUrlsAsync(PaginationRequest request)
    {
        var urlsQuery = _urlRepository.GetUrlsAsync();

        var totalUrlsCount = await urlsQuery.CountAsync();

        if (totalUrlsCount <= 0)
        {
            return new PaginationResponse<UrlListResponse>();
        }
            
        var paginatedUrls = await urlsQuery.ApplyPaging(request.PageNumber, request.PageSize).ToListAsync();
            
        var listResponse = _mapper.Map<List<Url>, List<UrlListResponse>>(paginatedUrls);

        return new PaginationResponse<UrlListResponse>
        {
            PagesCount = (int)Math.Ceiling((double)totalUrlsCount / request.PageSize),
            Items = listResponse
        };
    }

    public async Task<string> GetUrlByShortUrlValueAsync(string shortUrlValue)
    {
        var url = await _urlRepository.GetUrlByShortUrlAsync(shortUrlValue);

        if (url == null) throw new KeyNotFoundException("Url not found");

        return url.OriginalUrl;
    }
    
    public async Task<UrlResponse> GetUrlByIdAsync(int id)
    {
        var url = await _urlRepository.GetUrlByIdAsync(id);

        if (url == null) throw new KeyNotFoundException($"Url with id: {id} not found");
        
        var urlResponse = _mapper.Map<Url, UrlResponse>(url);

        return urlResponse;
    }

    public async Task CreateUrlAsync(UrlRequest request)
    {
        var url = await _urlRepository.GetUrlByOriginalUrlAsync(request.OriginalUrl);

        if (url != null) throw new ArgumentException($"Short url already exist: {url.ShortUrl}");

        var mapperUrl = _mapper.Map<UrlRequest, Url>(request);
        await _urlRepository.CreateUrlAsync(mapperUrl);
    }

    public async Task DeleteUrlByIdAsync(int id)
    {
        var url = await _urlRepository.GetUrlByIdAsync(id);
        
        if (url == null) throw new KeyNotFoundException($"Url with id: {id} not found");
        await _urlRepository.DeleteUrlAsync(url);
    }
}