using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao _userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        _postDao = postDao;
        _userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDTO postToCreate)
    {
        Post? existing = await _postDao.GetByTitle(postToCreate.title);
        Post? existing2 = await _postDao.GetByBody(postToCreate.body);

        User? user = await _userDao.GetByIdAsync(postToCreate.ownerID);

        if (postToCreate.title.Equals(existing) && postToCreate.body.Equals(existing2) && postToCreate.ownerID.Equals(user.id))
        {
            throw new Exception("This post already exists");
        }
        
        
        ValidateData(postToCreate);
        Post toCreate = new Post(user, postToCreate.title, postToCreate.body);
        Post created = await _postDao.CreateAsync(toCreate);

        return created;
    }

    private static void ValidateData(PostCreationDTO postToCreate)
    {
        string title = postToCreate.title;
        string body = postToCreate.body;
        if (title.Length < 3)
        {
            throw new Exception("Title must be at least 3 characters long!");
        }

        if (title.Length > 100)
        {
            throw new Exception("Title must be less that 100 characters long");
        }

        if (body.Length > 1000)
        {
            throw new Exception("Body must be less than 1000 characters long");
        }
    }
}