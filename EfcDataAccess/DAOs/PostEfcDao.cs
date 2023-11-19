using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly PostContext _context;

    public PostEfcDao(PostContext context)
    {
        _context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public  Task<Post?> GetByTitle(string title)
    {
        Post? existing =  _context.Posts.FirstOrDefault(u => u.title.ToLower().Equals(title.ToLower()));
        return Task.FromResult(existing);
    }

    public Task<Post?> GetByBody(string body)
    {
        Post? existing =  _context.Posts.FirstOrDefault(u => u.body.ToLower().Equals(body.ToLower()));
        return Task.FromResult(existing);
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? post = await _context.Posts.FindAsync(id);
        return post;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto)
    {
        IQueryable<Post> query = _context.Posts.AsQueryable();
    
        if (!string.IsNullOrEmpty(searchPostParametersDto.username))
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.username.ToLower().Equals(searchPostParametersDto.username.ToLower()));
            // we know username is unique, so just fetch the first
            query = query.Where(post =>
                user.username.ToLower().Equals(searchPostParametersDto.username.ToLower()));
        }
    
        if (searchPostParametersDto.userId != null)
        {
            query = query.Where(t => t.authorId == searchPostParametersDto.userId);
        }
    
        if (searchPostParametersDto.edited != null)
        {
            query = query.Where(t => t.edited == searchPostParametersDto.edited);
        }
    
        if (!string.IsNullOrEmpty(searchPostParametersDto.titleContains))
        {
            query = query.Where(t =>
                t.title.ToLower().Contains(searchPostParametersDto.titleContains.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(searchPostParametersDto.bodyContains))
        {
            query = query.Where(t =>
                t.body.ToLower().Contains(searchPostParametersDto.bodyContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
        
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }
}