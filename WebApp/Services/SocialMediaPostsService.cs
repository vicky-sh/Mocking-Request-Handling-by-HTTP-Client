using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Services;

public class SocialMediaPostsService(IHttpClientFactory httpClientFactory)
    : ISocialMediaPostsService
{
    private const string BaseUrl = "https://dummyjson.com";

    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(
        Constants.DUMMY_JSON_CLIENT
    );

    public async Task<IEnumerable<string>?> GetAllTagsForPostAsync(
        int postId,
        CancellationToken cancellationToken
    )
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/posts", cancellationToken);

        response.EnsureSuccessStatusCode();

        var posts = await response.Content.ReadFromJsonAsync<PostsResponse>(
            ObjectSerializer.GetOptions,
            cancellationToken
        );

        return posts!.Posts.FirstOrDefault(x => x.Id == postId)?.Tags;
    }
    
    public async Task<Post> AddPostAsync(
        Post post,
        CancellationToken cancellationToken
    )
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"{BaseUrl}/posts/add",
            post,
            ObjectSerializer.GetOptions,
            cancellationToken
        );

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Post>(
            ObjectSerializer.GetOptions,
            cancellationToken
        ) ?? throw new InvalidOperationException("Failed to deserialize the post.");
    }
}
