using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;

namespace FilmProject.Presentation.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        private readonly IMovieLikeService _movieLikeService;
   

        public HomeController(ILogger<HomeController> logger, IMovieLikeService movieLikeService,IMovieService movieService, IEmailService emailService, IUserService userService,IMapper mapper)
        {
            _logger = logger;
            _movieService = movieService;
            _emailService = emailService;
            _userService = userService;
            _movieLikeService = movieLikeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Home/GetUserCount")]
        public async Task<IActionResult> GetUserCountAsync()
        {
            int userCount = await _userService.GetUserCountAsync();
            return Json(userCount);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            MovieDto movieDto = await _movieService.GetWithCategoryAsync(m => m.Id == id);
            MovieViewModel movie = _mapper.Map< MovieDto, MovieViewModel>(movieDto);
            return View(movie);
        }


        [HttpPost]
        public async Task<IActionResult> GivePoint(int score)
        {
            var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUser == null)
            {
                // movieLike ekleme yapılcak
                return BadRequest("Lütfen oy kullanmak için giriş yapınız");
            }
            // burada score yanı sıra movieId de gelmesi lazım ki 2 veriyi gönderelim
            // movieId geldi var sayıyorum.

            var movieId = 1;
            

            MovieLikeDto model = await _movieLikeService.GetAsync(x=>x.userId == currentUser && x.MovieId==movieId);
            if (model == null)
            {
                // movieLike ekleme yapılcak
             
            }
            else
            {
                model.Point = score;  
                _movieLikeService.Update(model);
            }

            return Ok();
        }


        public async Task<IActionResult> Profil()
        {
            return View();
        }

    }
}