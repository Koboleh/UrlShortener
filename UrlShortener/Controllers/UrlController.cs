using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs.Request;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers;

[ApiController]
[Route("/api/urls")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUrls(PaginationRequest request)
    {
        var urls = await _urlService.GetUrlsAsync(request);
        return Ok(urls);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUrlById(int id)
    {
        var url = await _urlService.GetUrlByIdAsync(id);
        return Ok(url);
    }
    
    [HttpGet("short/{urlValue}")]
    public async Task<IActionResult> GetUrlByValue(string value)
    {
        var url = await _urlService.GetUrlByShortUrlValueAsync(value);
        return Ok(url);
    }

    [Authorize(Policy = "IsUrlOwnerOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreateUrl(UrlRequest request)
    {
        await _urlService.CreateUrlAsync(request);
        return Ok();
    }
    
    [Authorize(Policy = "IsUrlOwnerOrAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUrl(int id)
    {
        await _urlService.DeleteUrlByIdAsync(id);
        return Ok();
    }
    
}