using FilmProject.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("TotalCommentCount")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCountOfTotalComment() 
        {
            var count = await _commentService.GetCountOfTotalComment();
            var result = new { Count = count };
            return Json(result);
        }
        [HttpGet]
        [Route("LastComments")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLastComments() // yorumlar son eklenen olarak sıralanıp listeleniyor.
        {
            var comments = await _commentService.GetAllAsync();
            var result = comments.OrderByDescending(x=>x.Created).ToList();
      
            return Json(result);
        }
        [HttpGet]
        [Route("MostLikedComment")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> MostLikedComment() // yorumlar son eklenen olarak sıralanıp listeleniyor.
        {
            var comments = await _commentService.GetAllAsync();
            var result = comments.OrderByDescending(x => x.LikeCount).FirstOrDefault();

            return Json(result);
        }
    }
}
