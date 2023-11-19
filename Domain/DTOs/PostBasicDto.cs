using Domain.Models;

namespace Domain.DTOs;

public class PostBasicDto
{
    public string title { get; set; }
    public string body { get; set; }
    public int authorId { get; set; }
    public bool edited { get; set; }
    public int id { get; set; }
    
    public int upVote { get; set; }
    public int downVote { get; set; }

    public PostBasicDto(int authorId, string title, string body, int upVote, int downVote, bool edited)
    {
        this.authorId = authorId;
        this.title = title;
        this.body = body;
        this.upVote = upVote;
        this.downVote = downVote;
        this.edited = edited;
    }

}