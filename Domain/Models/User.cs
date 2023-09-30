namespace Domain.Models;

public class User
{
    public string username { get; set; }
    public string password { get; set; }
    public List<Post> posts { get; set; }
    public List<Comment> comments { get; set; }


}