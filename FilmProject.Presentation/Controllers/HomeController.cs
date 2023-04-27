using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;

namespace FilmProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IEmailService emailService, IMapper mapper)
        {
            _logger = logger;
            _movieService = movieService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
    
            return View();
        }


        public async Task<IActionResult> Detail(int id)
        {
            MovieDto movieDto = await _movieService.GetWithCategoryAsync(m => m.Id == id);
            MovieViewModel movie = _mapper.Map< MovieDto, MovieViewModel>(movieDto);
            return View(movie);
        }

        public async Task<IActionResult> Profil()
        {
            return View();
        }

    }
}