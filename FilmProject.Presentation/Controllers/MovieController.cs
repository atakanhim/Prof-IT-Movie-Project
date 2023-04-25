using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Versioning;
using System.Collections;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private IMapper _mapper;
        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _mapper = mapper;
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetCount")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMovieCountAsync() // film sayısı
        {
            var count = await _movieService.GetMovieCountAsync();
            var result = new { Count = count };
            return Json(result);
        }
        [HttpGet]
        [Route("GetMostPopular/{id?}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMostPopularMovieAsync(int id = 0) // en yuksek puana(begeni) sahip filmi çeken kod
        {
            try
            {
                IEnumerable<MovieDto> movies = await _movieService.GetListWithCategoryAsync();
                IEnumerable<MovieViewModel> movieViewModel = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<MovieViewModel>>(movies);
                if (id > 0)
                    movieViewModel = movieViewModel.OrderByDescending(m => m.Ortalama).Take(id);
                else
                    movieViewModel = movieViewModel.OrderByDescending(m => m.Ortalama);
                return PartialView(@"~/Views/Home/_RenderMoviesPartialView.cshtml", movieViewModel);

            }
            catch
            {
                return Ok(new
                {
                    mesaj = "Geçersiz Id Değeri "
                });
            }
        }

        [HttpGet]
        [Route("GetMostCommentedMovie/{number?}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMostCommentedMovieAsync(int number=0) // en çok yoruma sahip filmi çeken fonksiyon.
        {

            IEnumerable<MovieDto> movies = await _movieService.GetListWithCategoryAsync();
            IEnumerable<MovieViewModel> movieViewModel = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<MovieViewModel>>(movies);
            if (number > 0)
                movieViewModel = movieViewModel.OrderByDescending(m => m.Comments.Count).Take(number);
            else
                movieViewModel = movieViewModel.OrderByDescending(m => m.Comments.Count);
            return PartialView(@"~/Views/Home/_RenderMoviesPartialView.cshtml", movieViewModel);
          
        }
      
        [HttpGet]
        [Route("Movie/{id}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMovieAsync(int id) // tek bir film , id ile . Detay sayfası için 
        {

            // daha sonra istersek bu id le o listelerde filtreleme yapabilriz.
            try
            {
                var movies = await _movieService.GetListWithCategoryAsync();
                var topRatedMovie = movies.OrderByDescending(m => m.Id).FirstOrDefault();

                return Json(movies);
            }
            catch
            {
                return Ok(new
                {
                    mesaj = "Geçersiz Id Değeri "
                });
            }
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
        [Route("MoviesWithCategory/{category?}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMoviesWithCategoryAsync(string category) // tum filmler , categoriler ile birlikte doner bunu viewmodel olarak gonderir.
        {
            IEnumerable<MovieDto> movies = await _movieService.GetListWithCategoryAsync(category);
            IEnumerable<MovieViewModel> movieViewModel = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<MovieViewModel>>(movies);
           

            return PartialView(@"~/Views/Home/_RenderMoviesPartialView.cshtml", movieViewModel);
        }
        [HttpGet]
        [Route("MoviesWithCategoryJson")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMoviesWithCategoryJsonAsync() // tum filmler , categoriler ile birlikte doner bunu json olarak gonderir.
        {

            IEnumerable<MovieDto> movies = await _movieService.GetListWithCategoryAsync();

            IEnumerable<MovieViewModel> movieViewModel = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<MovieViewModel>>(movies);

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(movieViewModel, settings);


            return Ok(json);

        }
        [HttpGet]
        [Route("GetLastMovies/{number?}")]

        public async Task<IActionResult> GetLastMoviesAsync(int number = 0) // son yuklenen 
        {

            IEnumerable<MovieDto> movies = await _movieService.GetListWithCategoryAsync();
            IEnumerable<MovieViewModel> movieViewModel = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<MovieViewModel>>(movies);
            if (number > 0)
                movieViewModel = movieViewModel.OrderByDescending(m => m.Created).Take(number);
            else
                movieViewModel = movieViewModel.OrderByDescending(m => m.Created);
            return PartialView(@"~/Views/Home/_RenderMoviesPartialView.cshtml", movieViewModel);
        }

        [HttpGet]
        [Route("GetLanguages")]

        public async Task<IActionResult> GetAllLanguagesAsync()// veritabanında kayıtlı filmlere ait diller
        {
            var languages = await _movieService.GetAllLanguagesAsync();
            var result = new { LanguagesList = languages };
            return Json(result);
        }
        [HttpGet]
        [Route("GetLanguage/{language?}")]
        public async Task<IActionResult> GetMovieByLanguageAsync(string language)
        {
            // Burada eğer herhangi bir dil parametresi gönderilmediyse Movies sayfasına yönlendirilip filmlerin hepsi listelenmiştir.
            // Veritabanında kayıtlı filmlerde olmayan bir dilde ise boş liste ve dil ismi gönderilip jquery kontrolü yapılması amaçlandı.
            // Veri bulunması halinde hem liste hem de dil ismi gönderilmiştir.
            if (language == null)
            {
                return RedirectToAction("Movies", "Movie");
            }
            var movies = await _movieService.GetMovieByLanguageAsync(language);
            var result = new { Movies = movies, Language = language };
            return Json(result);
        }



        [HttpPost]
        [Route("Create")]
        public IActionResult CreateMovie([FromBody] MovieViewModel movieViewModel) // Film Ekleme fonksiyonu 
        {
            MovieDto movie = _mapper.Map<MovieViewModel, MovieDto>(movieViewModel);

            _movieService.Add(movie);

            return Ok(movie);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieViewModel MovieViewModel) // Film Ekleme fonksiyonu 
        {
            MovieDto newmovie = _mapper.Map<MovieViewModel, MovieDto>(MovieViewModel);


            MovieDto? oldmovie = await _movieService.GetAsync(x => x.Id == newmovie.Id);
            if (oldmovie != null)
            {
                try
                {
                    oldmovie.MovieSummary = newmovie.MovieSummary;
                    _movieService.Update(oldmovie);
                }
                catch (Exception ex)
                {
                    //Loglama yapılabilir.hee
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Ok(new
                {
                    mesaj = "Film Bulunamadı "
                });
            }
            return Ok(oldmovie);
        }
    }
}
