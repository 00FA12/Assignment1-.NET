using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    Task<Comment> CreateAsync(CommentCreationDto commentCreationDto);

    Task<Comment> GetByIdAsync(int commentId);

    Task<IEnumerable<Comment?>> GetByPostIdAsync(int postId);
}