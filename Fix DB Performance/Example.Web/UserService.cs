using System.Net.Http.Json;
using Example.Api;

namespace Example.Web;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var userResponse = await _httpClient.GetAsync($"http://localhost:5124/users?email={email}");

        if (userResponse.IsSuccessStatusCode)
        {
            return await userResponse.Content.ReadFromJsonAsync<User>();
        }

        return null;
    }
    
    public async Task<User?> GetByIdAsync(Guid userId)
    {
        var userResponse = await _httpClient.GetAsync($"http://localhost:5124/users/{userId}");

        if (userResponse.IsSuccessStatusCode)
        {
            return await userResponse.Content.ReadFromJsonAsync<User>();
        }

        return null;
    }
    
    public async Task<List<User>?> SearchByUsername(string username)
    {
        var userResponse = await _httpClient.GetAsync($"http://localhost:5124/users?username={username}");

        return await userResponse.Content.ReadFromJsonAsync<List<User>>();
    }
}
