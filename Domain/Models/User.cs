using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User
{
    public string username { get; set; }
    public string password { get; set; }
    [Key]
    public int id { get; set; }
    public int securityLevel { get; set;}

}