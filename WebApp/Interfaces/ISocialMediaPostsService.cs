using WebApp.Models;

namespace WebApp.Interfaces;

public interface ISocialMediaPostsService
{
    Task<IEnumerable<string>?> GetAllTagsForPostAsync(
        int postId,
        CancellationToken cancellationToken
    );

    Task<Post> AddPostAsync(
        Post post,
        CancellationToken cancellationToken
    );
}
