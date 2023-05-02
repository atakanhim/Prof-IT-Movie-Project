using Microsoft.AspNetCore.Mvc;
using FilmProject.Application.Interfaces;
using System.Security.Claims;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class CommentLikeController : Controller
    {
        private readonly ICommentLikeService _commentLikeService;
        private readonly Logger<CommentController> _logger;
        public CommentLikeController(ICommentLikeService commentLikeService, Logger<CommentController> logger)
        {
            _commentLikeService = commentLikeService;
            _logger = logger;

        }

        public async Task<bool> IsCommentLiked(int commentId)
        {
            try
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(userId == null)
                {
                    _logger.LogError("Id değeri null olarak geldi");
                    return false;
                }
                var result = await _commentLikeService.IsCommentLike(userId, commentId);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
