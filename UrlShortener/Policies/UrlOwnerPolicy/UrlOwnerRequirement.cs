using Microsoft.AspNetCore.Authorization;

namespace UrlShortener.Policies.UrlOwnerPolicy;

public class UrlOwnerRequirement : IAuthorizationRequirement
{
}