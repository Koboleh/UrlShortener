using UrlShortener.DTOs.Request;
using UrlShortener.DTOs.Response;

namespace UrlShortener.Interfaces;

public interface IUserService
{
    Task<UserResponse?> UserLogin(LoginRequest request);
}