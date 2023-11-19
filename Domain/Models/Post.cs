using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Post
{
    public string title { get; private set; }
    public string body { get; private set; }
    public int authorId { get; init; }
    public bool edited { get; set; }
    [Key]
    public int id { get; set; }
    
    public int upVote { get; set; }
    public int downVote { get; set; }

    public Post(int authorId, string title, string body)
    {
        this.authorId = authorId;
        this.title = title;
        this.body = body;
        upVote = 0;
        downVote = 0;
        edited = false;
    }

    private Post(){}

}