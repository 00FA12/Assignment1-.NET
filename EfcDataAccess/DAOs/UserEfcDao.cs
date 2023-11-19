using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly PostContext _context;
    
    public UserEfcDao(PostContext context)
    {
        _context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        user.securityLevel = 4;
        EntityEntry<User> newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = await _context.Users.FirstOrDefaultAsync(u =>
            u.username.ToLower().Equals(username.ToLower())
        );
        return existing;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await _context.Users.FindAsync(id);
        return user;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchUserParameters)
    {
        IQueryable<User> query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(searchUserParameters.UsernameContains))
        {
            query = query.Where(user => user.username.ToLower().Contains(searchUserParameters.UsernameContains));
        }

        List<User> result = await query.ToListAsync();
        return result;
    }
}