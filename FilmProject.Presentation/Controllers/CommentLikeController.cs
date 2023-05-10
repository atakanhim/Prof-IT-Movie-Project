using Microsoft.AspNetCore.Mvc;
using FilmProject.Application.Interfaces;
using System.Security.Claims;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using FilmProject.Application.Contracts.Movie;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace FilmProject.Presentation.Controllers
{
  
    public class CommentLikeController : Controller
    {
        private readonly ICommentLikeService _commentLikeService;
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private IMapper _mapper;

        public CommentLikeController(ICommentLikeService commentLikeService, ILogger<HomeController> logger, IMapper mapper, IStringLocalizer<SharedResource> localizer)
        {
            _commentLikeService = commentLikeService;
            _logger = logger;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpGet]

        public async Task<bool> IsCommentLiked(int commentId)
        {
            try
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(userId == null)
                {
                    return false;
                }
                var result = await _commentLikeService.IsCommentLike(userId, commentId);
                return result;
            }
            catch (Exception)
            {
                _logger.LogError("comment 'like statue' not returned");
                return false;
            }
        }
        [HttpGet]
        //[Route("LikeCount")]
        public async Task<int> NumberOfCommentLike(int commentId)
        {
            try
            {
                int result = await _commentLikeService.NumberofCommentLike(commentId);
                return result;
            }
            catch (Exception)
            {
                _logger.LogError("comment 'like count' not returned");
                return 0;
            }
        }
         
        [HttpPost]
        //[Route("ChangeLike")]
        //[Authorize]
        public async Task<IActionResult> ChangeCommentLikeStatue([FromBody]CommentLikeViewModel commentLike)
        {
            try
            {
                CommentLikeDto newCommentLike = _mapper.Map<CommentLikeViewModel, CommentLikeDto>(commentLike);

                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (userId == null)
                {
                    return BadRequest(_localizer["like_comment_required_signin"].Value);
                }

                newCommentLike.userId = userId;
                await _commentLikeService.ChangeCommentLikeStatue(newCommentLike);
                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError("comment like statue not changed");
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Test([FromBody]CommentLikeViewModel commentLikeViewModel)
        {
            return Ok();
        }
    }
}
