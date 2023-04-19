using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
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

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateComment([FromBody] Comment model) // Yorum Ekleme fonksiyonu 
        {
           
            _commentService.Add(model);
            return Ok(model);
        }
        [HttpPut]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id) // Yorum Delete Statusu Degistirme fonksiyonu 
        {
            try
            {
                var comment = await _commentService.GetAsync(x => x.Id == id);
                if (comment != null)
                {
                    comment.IsDeleted = true;
                    _commentService.Update(comment);
                    return Ok(comment);
                }
                return Ok(new
                {
                    mesaj = "Yorum Bulunamadı "
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
