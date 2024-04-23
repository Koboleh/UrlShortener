using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using UrlShortener.Interfaces;

namespace UrlShortener.Policies.UrlOwnerPolicy;

public class UrlOwnerHandler : AuthorizationHandler<UrlOwnerRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUrlService _urlService;

    public UrlOwnerHandler(IHttpContextAccessor httpContextAccessor, IUrlService urlService)
    {
        _httpContextAccessor = httpContextAccessor;
        _urlService = urlService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UrlOwnerRequirement requirement)
    {
        
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return;
        }
        
        
        if (!int.TryParse(_httpContextAccessor.HttpContext.Request.RouteValues["id"].ToString(), out var urlId) ||
            !int.TryParse(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
        {
            return;
        }

        var isOwner = await _urlService.IsUrlOwnerAsync(urlId, userId);
        
        if (isOwner)
        {
            context.Succeed(requirement);
        }
    }
}