using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("CreateCategory")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddCategory([FromBody]Category category)
        {
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

        [HttpPost]
        [Route("UpdateCategory")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
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
