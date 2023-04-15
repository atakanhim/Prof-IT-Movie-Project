using FilmProject.Application.Interfaces;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilmProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _movieService.GetAllAsync();

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