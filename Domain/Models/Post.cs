namespace Domain.Models;

public class Post
{
    public string title { get; set; }
    public string body { get; set; }
    public User author { get; }
    public bool edited { get; set; }
    public List<Comment> comments { get; set; }

    public Post(string title, string body, User author)
    {
        this.title = title;
        this.body = body;
        this.author = author;
        edited = false;
    }
}