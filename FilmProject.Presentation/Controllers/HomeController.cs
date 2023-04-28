using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilmProject.Presentation.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IEmailService emailService, IUserService userService)
        {
            _logger = logger;
            _movieService = movieService;
            _emailService = emailService;
            _userService = userService;
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
        public async Task<IActionResult> Detail()
        {

            return View();
        }

        public async Task<IActionResult> Profil()
        {

            return View();
        }

    }
}