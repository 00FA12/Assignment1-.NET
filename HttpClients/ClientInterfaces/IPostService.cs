using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{ 
    Task CreateAsync(PostCreationDTO dto);
}