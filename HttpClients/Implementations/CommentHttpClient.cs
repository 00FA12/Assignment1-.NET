using System.Diagnostics;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class CommentHttpClient : ICommentService
{
    private readonly HttpClient _client;

    public CommentHttpClient(HttpClient client)
    {
        _client = client;
    }


    public async Task CreateAsync(CommentCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/comments", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

    public async Task<ICollection<Comment>> GetByPostIdAsync(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"comments/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Comment> comments = JsonSerializer.Deserialize<ICollection<Comment>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return comments;
    }

    public async Task<Comment> GetByIdAsync(int commentId)
    {
        HttpResponseMessage response = await _client.GetAsync($"Comments?commentId={commentId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Comment comment = JsonSerializer.Deserialize<Comment>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return comment;
    }
    
}