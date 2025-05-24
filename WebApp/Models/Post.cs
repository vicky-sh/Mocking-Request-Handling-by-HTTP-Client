namespace WebApp.Models;

public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public IEnumerable<string>? Tags { get; set; }
    public Reactions? Reactions { get; set; }
    public int Views { get; set; }
    public int UserId { get; set; }
}

public class Reactions
{
    public int Likes { get; set; }
    public int Dislikes { get; set; }
}
