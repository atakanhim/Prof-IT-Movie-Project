using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Presentation.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core_2_1.Controllers
{
    [Area("Inspinia")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private IMapper _mapper;
        private readonly IFileService _fileService;


        public MovieController(IMapper mapper,IMovieService movieService, IFileService fileService)
        {
            _fileService = fileService;

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
        public async Task<IActionResult> UpdateMoviePage(int id)
        {
            MovieDto movieDto = await _movieService.GetWithCategoryAsync(m => m.Id == id);
            UpdateMovieViewModel movie = _mapper.Map<MovieDto, UpdateMovieViewModel>(movieDto);
            return View(movie);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateMoviePage([FromForm] UpdateMovieViewModel movieViewModel) // Film update fonksiyonu 
        {
       
            if(ModelState.IsValid)
            {
                try
                {
                    if (movieViewModel.Photo != null)
                    {
                        var result = _fileService.SaveImage(movieViewModel.Photo);
                        if (result.Item1 == 1)
                            movieViewModel.PhotoPath = result.Item2;

                    }
                    // categoriler dışında update başarılı
                    MovieDto movie = _mapper.Map<UpdateMovieViewModel, MovieDto>(movieViewModel);

                    _movieService.Update(movie);



                }
                catch (Exception ex)
                {
                    BadRequest(ex.Message);
                }
            }
  
            // return partial view model donucek 
            //jquery ile yakalayıp gondericez


            return View(movieViewModel);

        }

    }
}
