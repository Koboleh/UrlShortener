using UrlShortener.DataAccess.DbContexts;
using UrlShortener.DataAccess.Entities;
using UrlShortener.Interfaces;

namespace UrlShortener.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UrlShortenerDbContext _dbContext;

    public UserRepository(UrlShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }
}