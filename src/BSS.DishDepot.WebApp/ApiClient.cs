using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using System.Text.Json;

namespace BSS.DishDepot.WebApp
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<UserResponse>> RegisterUser(PostUserRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("users", request);
            return await HandleResponse<UserResponse>(result);
        }

        public async Task<Result<AccessToken>> Login(AuthenticateUserRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("users/authenticate", request);
            return await HandleResponse<AccessToken>(result);
        }

        private static async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var obj = JsonSerializer.Deserialize<T>(content);
                return Result<T>.Success(obj!);
            }

            return response.StatusCode switch 
            {
                System.Net.HttpStatusCode.Unauthorized => Result<T>.Unauthorized("Invalid username or password."),
                _ => Result<T>.Unexpected("An unexpected error occurred attempting to authenticate user.")
            };
        }
    }
}
