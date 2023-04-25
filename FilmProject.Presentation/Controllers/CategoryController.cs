using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private IMapper _mapper;
        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        [Route("CreateCategory")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddCategory([FromBody] CategoryViewModel categoryViewModel)
        {
            CategoryDto category = _mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
            try
            {
                _categoryService.AddCategory(category);
                return Ok();
            }
            catch (Exception ex) 
            {
                //Loglama yapılabilir.
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Categories")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMoviesWithCategoryAsync(string category) // tum filmler , categoriler ile birlikte doner bunu viewmodel olarak gonderir.
        {
            IEnumerable<CategoryDto> categories = await _categoryService.GetAllAsync();
            IEnumerable<CategoryViewModel> categoryViewModel = _mapper.Map<IEnumerable<CategoryDto>, IEnumerable<CategoryViewModel>>(categories);


            return PartialView(@"~/Views/Home/_RenderCategories.cshtml", categoryViewModel);
        }
        [HttpPost]
        [Route("UpdateCategory")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryViewModel categoryViewModel)
        {
            CategoryDto category = _mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
            try
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Ok();
            }
            catch (Exception ex)
            {
                //Loglama yapılabilir.
                return BadRequest(ex.Message);
            }
        }
    }
}
