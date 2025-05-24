namespace WebApp.Models;

public class PostsResponse
{
    public List<Post> Posts { get; set; } = [];
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}
