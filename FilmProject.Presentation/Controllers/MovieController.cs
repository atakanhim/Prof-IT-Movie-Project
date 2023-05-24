﻿using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Domain.Enums;

using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Versioning;
using System.Collections;
using System.Security.Claims;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieCategoryMapService _movieCategoryMapService;
        private readonly IFileService _fileService;
        private IMapper _mapper;

        public MovieController(IMovieService movieService,IMapper mapper, IFileService fileService, IMovieCategoryMapService movieCategoryMapService)
        {
            _mapper = mapper;
            _movieService = movieService;
            _fileService = fileService;
            _movieCategoryMapService = movieCategoryMapService;
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
        [Route("LikeAvarage/{id?}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetLikeAvarage(int id) // id si gelen filmin Like Ortalamasını doner
        {
            MovieDto movieDto = await _movieService.GetWithCategoryAsync(m => m.Id == id);
            MovieViewModel movie = _mapper.Map<MovieDto, MovieViewModel>(movieDto);
            return Json(movie.Ortalama);
        }
        [HttpGet]
        [Route("LikeCount/{id?}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetLikeCount(int id) // id si gelen filme kaç kişi oy kullanmis
        {
            MovieDto movieDto = await _movieService.GetWithCategoryAsync(m => m.Id == id);
            MovieViewModel movie = _mapper.Map<MovieDto, MovieViewModel>(movieDto);
            return Json(movie.MovieLikes.Count);
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
                {
                    movieViewModel = movieViewModel.OrderByDescending(m => m.Ortalama).Take(id).ToList();
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    var json = JsonConvert.SerializeObject(movieViewModel, settings);
                    return Ok(json);
                }
                else
                    movieViewModel = movieViewModel.OrderByDescending(m => m.Ortalama);

                movieViewModel = movieViewModel.Where(x => x.isDeleted == false);
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
            {
                movieViewModel = movieViewModel.OrderByDescending(m => m.Comments.Count).Take(number);
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var json = JsonConvert.SerializeObject(movieViewModel, settings);
                return Ok(json);
            }
               
            else
                movieViewModel = movieViewModel.OrderByDescending(m => m.Comments.Count);

            movieViewModel = movieViewModel.Where(x => x.isDeleted == false);
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
                return BadRequest(new
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
        public async Task<IActionResult> GetMoviesWithCategoryAsync(string category) // tum filmler yada category ile ,viewmodel olarak gonderir.
        {
            IEnumerable<MovieDto> movies = await _movieService.GetListWithCategoryAsync(category);
            IEnumerable<MovieViewModel> movieViewModel = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<MovieViewModel>>(movies);
            movieViewModel = movieViewModel.Where(x => x.isDeleted == false);

            return PartialView(@"~/Views/Home/_RenderMoviesPartialView.cshtml", movieViewModel);
        }
        [HttpGet]
        [Route("MoviesWithCategoryForAdmin/{category?}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMoviesWithCategoryForAdminAsync(string category) // tum filmler yada category ile, admin için
        {
            IEnumerable<MovieDto> movies = await _movieService.GetListWithCategoryAsync(category);
            IEnumerable<MovieViewModel> movieViewModel = _mapper.Map<IEnumerable<MovieDto>, IEnumerable<MovieViewModel>>(movies);
            movieViewModel=movieViewModel.OrderBy(x => x.isDeleted);
            return PartialView(@"~/Views/Shared/_RenderMoviesForAdmin.cshtml", movieViewModel);
 
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
            {
                movieViewModel = movieViewModel.OrderByDescending(m => m.Created).Take(number);
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var json = JsonConvert.SerializeObject(movieViewModel, settings);
                return Ok(json);
            }    
            else
                movieViewModel = movieViewModel.OrderByDescending(m => m.Created);


            movieViewModel = movieViewModel.Where(x => x.isDeleted == false);
            return PartialView(@"~/Views/Home/_RenderMoviesPartialView.cshtml", movieViewModel);
        }

        [HttpGet]
        [Route("GetLanguages")]

        public async Task<IActionResult> GetAllLanguagesAsync()// veritabanında kayıtlı filmlere ait diller
        {
            // bu kod veritabanına çıkarılacak
            var languages = await _movieService.GetAllLanguagesAsync();
            var arrayList =new ArrayList();
            foreach (var language in languages)
            {
                var movies = await _movieService.GetAllAsync(x => x.MovieLanguage == language);
                var languageName = "";
                switch (language)
                {
                    case "tr-TR":
                        languageName = "Türkçe";
                        break;
                    case "en-US":
                        languageName = "İngilizce";
                        break;
                    default:
                        languageName = language;
                        break;
                }

                var arrayListItem = new
                {
                    language = languageName,
                    count = movies.Count()
                };
                arrayList.Add(arrayListItem);

            }
            return Json(arrayList);
        }
        [HttpGet]
        [Route("GetRatingAge")]

        public async Task<IActionResult> GetRatingAgeAsync()// veritabanında kayıtlı filmlere ait diller
        {
             
           
            var arrayList = new ArrayList();
            for (int i = 0; i < Enum.GetValues(typeof(MovieRatings)).Length; i++)
            {
                var rating = (MovieRatings)i;
                var ratingAgeList = await _movieService.GetAllAsync(x => x.RatingAge == rating);

                var arrayListItem = new
                {
                    ratingAge = rating.ToString(),
                    count = ratingAgeList.Count()
                };

                arrayList.Add(arrayListItem);
            }

            return Json(arrayList);
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
        public IActionResult CreateMovie([FromForm] PostMovieViewModel postMovieViewModel) // Film Ekleme fonksiyonu 
        {

            try 
            {
                var result = _fileService.SaveImage(postMovieViewModel.Photo);
                if (result.Item1 == 1)
                {
                    postMovieViewModel.PhotoPath = result.Item2;
                }
                
                MovieDto movie = _mapper.Map<PostMovieViewModel, MovieDto>(postMovieViewModel);

                var movieId = _movieService.Add(movie);
                _movieCategoryMapService.AddMovieToCategory(movieId, postMovieViewModel.CategoriesId);
                TempData["SuccessMessage"] = movie.MovieName;

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = postMovieViewModel.MovieName;
            }
            
            return RedirectToAction("Index","Inspinia");
            
        }

   

        [HttpPost]
        [Route("AddToArchive")]
        public async Task<IActionResult> AddToArchiveAsync([FromBody] MovieViewModel movieViewModel) // Film arşive ekleme fonksiyonu
        {
            try
            {
                MovieDto model = await _movieService.GetAsync(x=>x.Id == movieViewModel.Id);
                if (model != null)
                {
                    model.isDeleted = !model.isDeleted;
                    _movieService.Update(model);

                    return Ok(model.isDeleted);
                }
                return BadRequest("Arşive ekleme/cikarma işlemi başarısız .");
            }
            catch (Exception ex)
            {
                return BadRequest("Arşive ekleme/cikarma işlemi başarısız .");
            }
        }
        [HttpPost]
        [Route("ChangeIsHighLighted")]
        [Authorize]
        public async Task<IActionResult> ChangeIsHighLightedAsync([FromBody] MovieViewModel movieModel)
        {
            try
            {
                MovieDto model = await _movieService.GetAsync(x => x.Id == movieModel.Id);
                if (model != null)
                {
                    model.IsHighLighted = !model.IsHighLighted;
                    _movieService.Update(model);

                    return Ok(model.IsHighLighted);
                }
                return BadRequest("Model bulunamadı.");
            }
            catch (Exception ex)
            {
                return BadRequest("Öne çıkarma/gerialama işlemi başarısız.");
            }
        }
    }
}
