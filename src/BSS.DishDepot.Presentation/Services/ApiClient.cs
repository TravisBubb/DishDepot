using BSS.DishDepot.Application.Dto;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BSS.DishDepot.Presentation.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _accessor;
    private readonly ITokenService _tokenService;
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    private const string BaseUrl = "http://bss.dishdepot.api/api/";

    public ApiClient(HttpClient httpClient, IHttpContextAccessor accessor, ITokenService tokenService)
    {
        _httpClient = httpClient;
        _accessor = accessor;
        _tokenService = tokenService;
    }

    public async Task<Result<UserResponse>> RegisterUser(PostUserRequest request)
    {
        return await PostAsync<PostUserRequest, UserResponse>("users", request);
    }

    public async Task<Result<AccessToken>> Login(AuthenticateUserRequest request)
    {
        return await PostAsync<AuthenticateUserRequest, AccessToken>("users/authenticate", request);
    }

    public async Task<Result<RecipesResponse>> GetRecipes()
    {
        var token = GetToken();
        return await GetAsync<RecipesResponse>("recipes", token);
    }

    public async Task<Result<RecipeResponse>> GetRecipe(Guid recipeId)
    {
        var token = GetToken();
        return await GetAsync<RecipeResponse>($"recipes/{recipeId}", token);
    }

    public async Task<Result<RecipeResponse>> CreateRecipe(PostRecipeRequest request)
    {
        var token = GetToken();
        return await PostAsync<PostRecipeRequest, RecipeResponse>("recipes", request, token);
    }

    private string GetToken()
    {
        var user = _accessor.HttpContext.User;
        if (user?.Claims is null)
            throw new UnauthorizedAccessException("User is not signed in.");

        return _tokenService.GetToken(user.Claims.ToList());
    }

    private async Task<Result<TResponse>> GetAsync<TResponse>(string path, string? token = null)
    {
        try
        {
            using var httpRequest = new HttpRequestMessage 
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BaseUrl + path)
            };

            if (!string.IsNullOrWhiteSpace(token))
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await _httpClient.SendAsync(httpRequest);
            return await HandleResponse<TResponse>(result);
        }
        catch (Exception ex)
        {
            return Result<TResponse>.Unexpected("An unexpected error occurred attempting to communicate with server.", ex);
        }
    }

    private async Task<Result<TResponse>> PostAsync<TRequest, TResponse>(string path, TRequest request, string? token = null)
    {
        try
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(request, SerializerOptions), Encoding.UTF8, "application/json");

            using var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = jsonContent,
                RequestUri = new Uri(BaseUrl + path)
            };

            if (!string.IsNullOrWhiteSpace(token))
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await _httpClient.SendAsync(httpRequest);
            return await HandleResponse<TResponse>(result);
        }
        catch (Exception ex)
        {
            return Result<TResponse>.Unexpected("An unexpected error occurred attempting to communicate with server.", ex);
        }
    }

    private static async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<T>(content, SerializerOptions);
            return Result<T>.Success(obj!);
        }

        return response.StatusCode switch 
        {
            System.Net.HttpStatusCode.Unauthorized => Result<T>.Unauthorized("Invalid username or password."),
            _ => Result<T>.Unexpected("An unexpected error occurred attempting to authenticate user.")
        };
    }
}
