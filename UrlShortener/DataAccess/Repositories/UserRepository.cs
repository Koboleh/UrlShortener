using Microsoft.EntityFrameworkCore;
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
    
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}