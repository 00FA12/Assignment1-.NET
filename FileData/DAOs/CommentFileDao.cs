using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class CommentFileDao : ICommentDao
{
    private readonly FileContext _context;

    public CommentFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Comment> CreateAsync(Comment comment)
    {
        int id = 1;
        if (_context.Comments.Any())
        {
            id = _context.Comments.Max(c => c.id);
            id++;
        }

        comment.id = id;
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return Task.FromResult(comment);
    }

    public Task<Comment?> GetByContentAsync(string text)
    {

        Comment? existing = _context.Comments.FirstOrDefault(c => c.text.Equals(text, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<Comment?>> GetByPostIdAsync(int postId)
    {
        IEnumerable<Comment?> comments = _context.Comments.Where(c => c.post.id == postId);
        return Task.FromResult(comments);
    }

    public Task<Comment?> GetByIdAsync(int commentId)
    {
        Comment? comment = _context.Comments.FirstOrDefault(c => c.id == commentId);
        return Task.FromResult(comment);
    }
}