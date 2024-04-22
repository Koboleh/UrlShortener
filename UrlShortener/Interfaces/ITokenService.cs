using UrlShortener.DataAccess.Entities;

namespace UrlShortener.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}