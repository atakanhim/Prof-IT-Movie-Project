using AutoMapper;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class MovieCategoryMapController : Controller
    {
        private readonly IMovieCategoryMapService _movieCategoryMapService;
        private IMapper _mapper;

        public MovieCategoryMapController(IMovieCategoryMapService movieCategoryMapService, IMapper mapper)
        {
            _mapper = mapper;
            _movieCategoryMapService = movieCategoryMapService;
        }

        [HttpGet]
        [Route("AnyMoviesInThis/{categoryId}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> AnyMoviesInThisCategory(int categoryId) // kategoriler listeleniyor sayısı
        {

            var result = await _movieCategoryMapService.AnyMoviesInThisCategory(x => x.CategoryId == categoryId);


            return Json(result);
        }
    }
}