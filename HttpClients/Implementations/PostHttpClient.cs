using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient _client;

    public PostHttpClient(HttpClient _client)
    {
        this._client = _client;
    }


    public async Task CreateAsync(PostCreationDTO dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync($"/posts", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Post>> GetAsync(string? userName, bool? editedStatus, string? titleContains, string? bodyContains)
    {
        string query = ConstructQuery(userName, editedStatus, titleContains, bodyContains);
        
        HttpResponseMessage response = await _client.GetAsync("/posts" + query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;

    }

    private static string ConstructQuery(string? userName, bool? editedStatus, string? titleContains, string? bodyContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(userName))
        {
            query += $"?username={userName}";
        }

        if (editedStatus != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"edited={editedStatus}";
        }

        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titlecontains={titleContains}";
        }

        if (!string.IsNullOrEmpty(bodyContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"bodycontains={bodyContains}";
        }

        return query;

    }
}