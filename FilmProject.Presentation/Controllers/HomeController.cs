
﻿using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;

﻿using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;

using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;

namespace FilmProject.Presentation.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
   

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IEmailService emailService, IUserService userService,IMapper mapper)
        {
            _logger = logger;
            _movieService = movieService;
            _emailService = emailService;
            _userService = userService;
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
            return Ok();
        }


        public async Task<IActionResult> Profil()
        {
            return View();
        }

    }
}