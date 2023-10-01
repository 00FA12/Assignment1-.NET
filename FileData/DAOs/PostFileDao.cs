using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext _context;

    public PostFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (_context.Posts.Any())
        {
            id = _context.Posts.Max(t => t.id);
            id++;
        }

        post.id = id;
        _context.Posts.Add(post);
        _context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<Post?> GetByTitle(string title)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.title.Equals(title, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<Post?> GetByBody(string body)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.body.Equals(body, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);    }

    public Task<Post> GetByIdAsync(int id)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.id == id);
        return Task.FromResult(existing);
    }
}