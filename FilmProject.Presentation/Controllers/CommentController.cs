﻿using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _mapper = mapper;
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
        public async Task<IActionResult> MostLikedComment() // en çok begenilen yorum listeleniyor
        {
            var comments = await _commentService.GetAllAsync();
            var result = comments.OrderByDescending(x => x.LikeCount).FirstOrDefault();

            return Json(result);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateComment([FromBody] CommentViewModel commentViewModel) // Yorum Ekleme fonksiyonu 
        {
            commentViewModel.userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
