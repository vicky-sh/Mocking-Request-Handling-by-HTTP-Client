using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SocialMediaPostsController(ISocialMediaPostsService socialMediaPostsService) : ControllerBase
{
    /// <summary>
    ///     Gets all tags for a specific post.
    /// </summary>
    /// <param name="postId">The ID of the post.</param>
    /// <param name="service">The social media posts service.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of tags or a bad request if not found.</returns>
    [HttpGet]
    [Route("{postId}/tags")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<string>>> GetAllPosts(
        [FromRoute] int postId,
        CancellationToken cancellationToken
    )
    {
        var tags = await socialMediaPostsService.GetAllTagsForPostAsync(postId, cancellationToken);
        return tags is null ? BadRequest("resource could not be found") : Ok(tags);
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> PostAsync(
        [FromBody] [Required] Post post,
        CancellationToken cancellationToken
    )
    {
        return await socialMediaPostsService.AddPostAsync(post, cancellationToken);
    }
}
