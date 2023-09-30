using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UsersController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }


    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDTO dto)
    {
        try
        {
            User user = await _userLogic.CreateAsync(dto);
            return Created($"/users/{user.username}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}