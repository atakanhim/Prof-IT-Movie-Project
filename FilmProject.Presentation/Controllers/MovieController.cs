using FilmProject.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("MovieCount")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMovieCountAsync() // film sayısı
        {
            var count = await _movieService.GetMovieCountAsync();
            var result = new { Count = count };
            return Json(result);
        }

        [HttpGet]
        [Route("Movies")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMoviesAsync() // tum filmler 
        {
            var movies = await _movieService.GetAllAsync();
           
            return Json(movies);
        }
        [HttpGet]
        [Route("GetLastMovies/{number}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetLastMoviesAsync(int number) // son yuklenen 
        {
            var movies = await _movieService.GetLastMoviesAsync(number);

            return Json(movies);
        }
    }
}
