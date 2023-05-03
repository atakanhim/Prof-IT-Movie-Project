using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Presentation.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core_2_1.Controllers
{
    [Area("Inspinia")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private IMapper _mapper;
        public MovieController( IMapper mapper,IMovieService movieService)
        {
            _movieService = movieService;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetMovieDetay(int id)
        {
            MovieDto movieDto = await _movieService.GetWithCategoryAsync(m => m.Id == id);
            MovieViewModel movie = _mapper.Map<MovieDto, MovieViewModel>(movieDto);
            return View(movie);
        }
        public IActionResult GetAddMovieForm() 
        { 
            return View();
        }
        public IActionResult GetMovieList()
        {
            return View();
        }

       
    }
}
