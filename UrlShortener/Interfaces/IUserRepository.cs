using UrlShortener.DataAccess.Entities;

namespace UrlShortener.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task UpdateUser(User user);
}