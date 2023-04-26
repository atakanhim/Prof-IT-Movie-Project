using FilmProject.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Areas.Inspinia.Controllers
{
    [Area("Inspinia")]
    public class AdminCategoryController : Controller
    {
        public IActionResult CategoryAdd()
        {
            return View();
        }
        
      
    }
}
