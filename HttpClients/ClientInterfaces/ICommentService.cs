using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task CreateAsync(CommentCreationDto dto);

    Task<ICollection<Comment>> GetByPostIdAsync(int id);

    Task<Comment> GetByIdAsync(int commentId);
}