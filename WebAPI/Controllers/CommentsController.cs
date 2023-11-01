using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentLogic commentLogic;

    public CommentsController(ICommentLogic commentLogic)
    {
        this.commentLogic = commentLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateAsync(CommentCreationDto commentCreationDto)
    {
        try
        {
            Comment comment = await commentLogic.CreateAsync(commentCreationDto);
            return Created($"/comments/{comment.id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Comment?>> GetByIdAsync([FromQuery] int commentId)
    {
        try
        {
            Comment? comment = await commentLogic.GetByIdAsync(commentId);
            return Ok(comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetByPostIdAsync([FromRoute] int id)
    {
        try
        {
            IEnumerable<Comment?> comments = await commentLogic.GetByPostIdAsync(id);
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}