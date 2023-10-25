using System.ComponentModel.DataAnnotations;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IList<User> users = new List<User>
    {
        new User
        {
            Password = "onetwo3FOUR",
            Username = "trmo",
            SecurityLevel = 4
        },
        new User
        {
            Password = "password",
            Username = "jknr",
            SecurityLevel = 2
        }
    };

    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => 
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }
    

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        users.Add(user);
        
        return Task.CompletedTask;
    }
}