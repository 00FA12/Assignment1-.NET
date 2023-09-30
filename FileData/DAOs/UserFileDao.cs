using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext _context;

    public UserFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        foreach (var u in _context.Users)
        {
            if (u.username.Equals(user.username))
            {
                throw new Exception("Username already taken!");
            }
        }
        _context.Users.Add(user);
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = _context.Users.FirstOrDefault(u => u.username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }
}