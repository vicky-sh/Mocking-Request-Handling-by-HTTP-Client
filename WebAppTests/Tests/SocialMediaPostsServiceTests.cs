using System.Net;
using AutoBogus;
using Shouldly;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Services;
using WebApplicationTests.TestHelpers;

namespace WebApplicationTests.Tests;

public class SocialMediaPostsServiceTests : MockHttpMessageHandlerTestBase
{
    private readonly ISocialMediaPostsService _socialMediaPostsServiceMock;

    public SocialMediaPostsServiceTests()
    {
        _socialMediaPostsServiceMock = new SocialMediaPostsService(HttpClientFactory);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetAllTagsForPostTestAsync(int postId)
    {
        var posts = GetAllPosts();
        HttpMockHandlerReturns(posts, HttpStatusCode.OK);
        HttpMockHandlerShouldHandleFor(HttpMethod.Get, "dummyjson.com/posts");
        var tags = await _socialMediaPostsServiceMock.GetAllTagsForPostAsync(
            postId,
            CancellationToken.None
        );
        tags.ShouldBeEquivalentTo(posts.Posts.Find(x => x.Id == postId)?.Tags);
    }

    [Fact]
    public async Task AddPostTestAsync()
    {
        var postToSend = new AutoFaker<Post>().UseSeed(42).Generate();
        var postReceived = new AutoFaker<Post>().UseSeed(42).Generate();
        HttpMockHandlerReturns(postReceived, HttpStatusCode.OK);
        HttpMockHandlerShouldHandleFor(HttpMethod.Post, "dummyjson.com/posts/add");
        var post = await _socialMediaPostsServiceMock.AddPostAsync(
            postToSend,
            CancellationToken.None
        );
        
        post.ShouldNotBeNull();
        post.ShouldBeEquivalentTo(postReceived);
        HttpMockHandlerShouldReceiveObject(postToSend);
    }

    private static PostsResponse GetAllPosts()
    {
        return new PostsResponse
        {
            Posts =
            [
                new Post
                {
                    Id = 1,
                    Tags = new List<string> { "tag1", "tag2", "tag3", "tag4" }
                },
                new Post
                {
                    Id = 2,
                    Tags = new List<string> { "tag5", "tag6" }
                }
            ],
            Total = 2,
            Skip = 0,
            Limit = 10
        };
    }
}
