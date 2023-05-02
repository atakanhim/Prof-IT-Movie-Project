using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class MovieLikeController : Controller
    {
   
        private readonly IMovieLikeService _movieLikeService;
        private IMapper _mapper;

        public MovieLikeController(IMapper mapper, IMovieLikeService movieLikeService)
        {
            _mapper = mapper;
            _movieLikeService = movieLikeService;
       
        }
        [HttpPost]
        [Route("GivePoint")]
        public async Task<IActionResult> GivePointAsync([FromBody] MovieLikeViewModel viewmodel)
        {
            var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUser == null)
                return BadRequest("Lütfen oy kullanmak için giriş yapınız");

            MovieLikeDto oldMovie = await _movieLikeService.GetAsync(x => x.userId == currentUser && x.MovieId == viewmodel.MovieId);
            if (oldMovie == null)
            {
                // movieLike ekleme yapılcak
                viewmodel.userId = currentUser;
                MovieLikeDto newMovie = _mapper.Map<MovieLikeViewModel, MovieLikeDto>(viewmodel);
                _movieLikeService.Add(newMovie);
                return Ok("Film Puanladığınız için teşekürler");
            }
            else
            {
                oldMovie.Point = viewmodel.Point;
                _movieLikeService.Update(oldMovie);
                return Ok("Puanınız guncellendi");
            }


        }

        [HttpGet]
        [Route("GetPoint/{movieId}")]
        public async Task<IActionResult> GetPointAsync(int movieId)
        {
            var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUser == null)
                return BadRequest("KUllanıcı giriş yapılmadıgı için herhangi bir oy gozukmuyor");
         
            MovieLikeDto movieLikeDto = await _movieLikeService.GetAsync(x => x.userId == currentUser && x.MovieId == movieId);
            if (movieLikeDto == null)
                return Ok(0);// oy kullanmadıgı için
            return Ok(movieLikeDto.Point);
        }

    }
}
