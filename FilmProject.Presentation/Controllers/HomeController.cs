using FilmProject.Application.Interfaces;
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
        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IEmailService emailService)
        {
            _logger = logger;
            _movieService = movieService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        public async Task<IActionResult> Detail()
        {

            return View();
        }

        public async Task<IActionResult> Profil()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}