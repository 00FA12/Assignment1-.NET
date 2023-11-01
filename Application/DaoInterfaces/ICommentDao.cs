using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ICommentDao
{
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> GetByContentAsync(string text);

    Task<IEnumerable<Comment?>> GetByPostIdAsync(int postId);
    Task<Comment?> GetByIdAsync(int commentId);
}