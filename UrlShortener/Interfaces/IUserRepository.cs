using UrlShortener.DataAccess.Entities;

namespace UrlShortener.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
}