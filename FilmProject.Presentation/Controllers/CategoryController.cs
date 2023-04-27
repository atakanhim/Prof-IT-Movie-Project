using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryViewModel> _validator;
        private IMapper _mapper;
        public CategoryController(ICategoryService categoryService,IMapper mapper, IValidator<CategoryViewModel> validator)
        {

            _mapper = mapper;
            _categoryService = categoryService;
            _validator = validator;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("CreateCategory")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryViewModel categoryViewModel)
        {

            if (!ModelState.IsValid)
            {
                Exception ex = new Exception(
                    message: "Lutfen Kategori Adı Giriniz"
                    );
                return BadRequest(ex.Message);
            }

            CategoryDto category = _mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
            try
            {
                await _categoryService.AddCategory(category);
                return Json(new { success = true });
            }
            catch (Exception ex) 
            {
                //Loglama yapılabilir.
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ListAll")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetCategories() // film sayısı
        {
            var Liste = await _categoryService.GetAllAsync();
        
            return Json(Liste);
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
        public async Task<IActionResult> UpdateCategoryy([FromBody] CategoryViewModel categoryViewModel)
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

        [HttpGet]
        [Route("AllCategories")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                IEnumerable<CategoryDto> categories = await _categoryService.GetAllAsync();
                IEnumerable<CategoryViewModel> categoryViewModel = _mapper.Map<IEnumerable<CategoryDto>, IEnumerable<CategoryViewModel>>(categories);


                return Ok(categoryViewModel);
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
            

        [HttpPost]
        [Route("Delete")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //Loglama yapılabilir.
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryViewModel categoryViewModel)
        {
            CategoryDto category = _mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
            try
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //Loglama yapılabilir.
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }

        }
    }
}
