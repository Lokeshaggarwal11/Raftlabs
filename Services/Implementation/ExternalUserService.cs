using Microsoft.Extensions.Options;
using RaftLabs.ReqResApiClient.Configuration;
using RaftLabs.ReqResApiClient.Models;
using RaftLabs.ReqResApiClient.Services.Abstraction;

namespace RaftLabs.ReqResApiClient.Services.Implementation
{
    public class ExternalUserService(HttpClient httpClient, IOptions<ApiOptions> options) : IExternalUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _baseUrl = options.Value.BaseUrl.TrimEnd('/');

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/users/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }

            var json = await response.Content.ReadFromJsonAsync<SingleApiResponse<User>>();
            return json?.Data;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            int page = 1;
            int totalPages;

            do
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/users?page={page}");
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Failed to get users from page {page}");

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<User>>();
                if (result?.Data == null) break;

                users.AddRange(result.Data);
                totalPages = result.Total_Pages;
                page++;
            } while (page <= totalPages);

            return users;
        }
    }
}
