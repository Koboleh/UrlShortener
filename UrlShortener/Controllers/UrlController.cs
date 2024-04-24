using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs.Request;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers;

[ApiController]
[Route("/api")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpGet("urls")]
    public async Task<IActionResult> GetUrls([FromQuery] PaginationRequest request)
    {
        var urls = await _urlService.GetUrlsAsync(request);
        return Ok(urls);
    }
    
    [HttpGet("urls/{id}")]
    public async Task<IActionResult> GetUrlById(int id)
    {
        var url = await _urlService.GetUrlByIdAsync(id);
        return Ok(url);
    }
    
    [HttpGet("{value}")]
    public async Task<IActionResult> GetUrlByValue(string value)
    {
        var url = await _urlService.GetUrlByShortUrlValueAsync(value);
        return Redirect(url);
    }

    [Authorize(Policy = "IsUrlOwnerOrAdmin")]
    [HttpPost("urls")]
    public async Task<IActionResult> CreateUrl(UrlRequest request)
    {
        await _urlService.CreateUrlAsync(request);
        return Ok();
    }
    
    [Authorize(Policy = "IsUrlOwnerOrAdmin")]
    [HttpDelete("urls/{id}")]
    public async Task<IActionResult> DeleteUrl(int id)
    {
        await _urlService.DeleteUrlByIdAsync(id);
        return Ok();
    }
    
}