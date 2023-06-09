﻿using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Localization;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CommentController(ICommentService commentService, IMapper mapper, IStringLocalizer<SharedResource> localizer)
        {
            _mapper = mapper;
            _commentService = commentService;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("AllComments")]
        public async Task<IActionResult> GetAllCommentsAsync()//Tüm Yorumları Döndürür
        {
            try
            {
                IEnumerable<AdminCommentDto> comments = await _commentService.GetListWithAppUserAndMovie();
                IEnumerable<AdminCommentViewModel> commentViewModels = _mapper.Map<IEnumerable<AdminCommentDto>, IEnumerable<AdminCommentViewModel>>(comments);

                return Json(new { data = commentViewModels });
            }
            catch
            {
                return BadRequest("Yorum Bulunamadı.");
            }
        }


        [HttpGet]
        [Route("List/{id}")]
        public async Task<IActionResult> GetCommentsWithMovieIdAsync(int id) // tum filmler , categoriler ile birlikte doner bunu viewmodel olarak gonderir.
        {
            IEnumerable<CommentDto> comments = await _commentService.GetListWithAppUser(x=>x.MovieId == id && x.IsConfirm==true && x.IsDeleted==false);
            comments = comments.OrderBy(x=>x.Created).ToList();

            IEnumerable<CommentViewModel> commentViewModels = _mapper.Map<IEnumerable<CommentDto>, IEnumerable<CommentViewModel>>(comments);


            return PartialView(@"~/Views/Home/_RenderComments.cshtml", commentViewModels);
        }
        [HttpGet]
        [Route("Count/{id}")]
        public async Task<IActionResult> GetCommentCount(int id) // tum filmler , categoriler ile birlikte doner bunu viewmodel olarak gonderir.
        {
            IEnumerable<CommentDto> comments = await _commentService.GetListWithAppUser(x => x.MovieId == id && x.IsConfirm==true && x.IsDeleted==false);
            return Json(comments.Count());
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
        public async Task<IActionResult> MostLikedComment() // en çok begenilen yorum listeleniyor
        {
            var comments = await _commentService.GetAllAsync();
            var result = comments.OrderByDescending(x => x.CommentLikes.Count).FirstOrDefault();

            return Json(result);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateComment([FromBody] CommentCreateViewModel commentCreateViewModel) // Yorum Ekleme fonksiyonu 
        {
            

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Exception ex = new Exception(
                        message: _localizer["comment_empty_error"]
                        );
                return BadRequest(ex.Message);



            }
            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.Content = commentCreateViewModel.Content;
            commentViewModel.MovieId = commentCreateViewModel.MovieId;
            commentViewModel.userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


           

            var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                return BadRequest(_localizer["write_comment_required_signin"].Value);
            }

            

            CommentDto comment = _mapper.Map<CommentViewModel, CommentDto>(commentViewModel);

            _commentService.Add(comment);
            return Json(comment);
        }

        [HttpPut]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id) // Yorum Delete Statusu Degistirme fonksiyonu 
        {
            try
            {
                var comment = await _commentService.GetAsync(x => x.Id == id && x.IsDeleted==false) ;
                if (comment != null)
                {
                    comment.IsDeleted = !comment.IsDeleted;
                    _commentService.Update(comment);
                    return Json(comment);
                }
                return BadRequest("Yorum Bulunamadı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
       [Route("ChangeState/{id}")] // Yorum Confirm Statusu Degistirme fonksiyonu 
        public async Task<IActionResult> ChangeCommentStatue(int id)
        {
            try
            {
                var comment = await _commentService.GetAsync(x => x.Id == id);
                if(comment != null)
                {
                    comment.IsConfirm = !comment.IsConfirm;
                    _commentService.Update(comment);
                    return Json(comment);
                }
                else
                {
                    return Ok(new
                    {
                        mesaj = "Yorum bulunamadı"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
