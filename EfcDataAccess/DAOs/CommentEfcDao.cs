using Application.DaoInterfaces;
using Domain.Models;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    public Task<Comment> CreateAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task<Comment?> GetByContentAsync(string text)
    {
        throw new NotImplementedException();
    }
}