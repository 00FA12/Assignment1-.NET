using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{ 
    Task CreateAsync(PostCreationDTO dto);
    
    Task<ICollection<Post>> GetAsync(
        string? userName, 
        int? userId, 
        bool? edited, 
        string? titleContains
    );

}