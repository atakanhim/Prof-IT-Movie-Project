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
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;



        public HomeController(ILogger<HomeController> logger,IMovieService movieService, IEmailService emailService, IUserService userService,IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _movieService = movieService;
            _userService = userService;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole("Admin"));
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



        public async Task<IActionResult> Profil()
        {
            return View();
        }

    }
}