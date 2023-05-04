using Microsoft.AspNetCore.Mvc;
using FilmProject.Application.Interfaces;
using System.Security.Claims;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using FilmProject.Application.Contracts.Movie;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace FilmProject.Presentation.Controllers
{
  
    public class CommentLikeController : Controller
    {
        private readonly ICommentLikeService _commentLikeService;
        private readonly ILogger<HomeController> _logger;
        private IMapper _mapper;

        public CommentLikeController(ICommentLikeService commentLikeService, ILogger<HomeController> logger, IMapper mapper)
        {
            _commentLikeService = commentLikeService;
            _logger = logger;
            _mapper = mapper;
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
                    return BadRequest("Yorum Beğenmek İçin Önce Giriş Yapmalısınız");
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
