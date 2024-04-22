using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UrlShortener.DataAccess.Entities;
using UrlShortener.Interfaces;

namespace UrlShortener.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _accessTokenKey;
    private readonly ILogger<TokenService> _logger;
    
    public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _accessTokenKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtTokenConfiguration:AccessTokenKey"]!));
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(JwtRegisteredClaimNames.Exp, 
                new DateTimeOffset(DateTime.UtcNow
                        .AddMinutes(Convert.ToDouble(_configuration["JwtTokenConfiguration:AccessTokenExpirationMinutes"])))
                    .ToUnixTimeSeconds().ToString())
        };
        
        var credentials = new SigningCredentials(_accessTokenKey,
            SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtTokenConfiguration:AccessTokenExpirationMinutes"])),
            SigningCredentials = credentials,
            Issuer = _configuration["JwtTokenConfiguration:Issuer"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var accessToken = tokenHandler.WriteToken(token);

        _logger.LogInformation("Access token created for user: {UserName}", user.Username);

        return accessToken;
    }
}