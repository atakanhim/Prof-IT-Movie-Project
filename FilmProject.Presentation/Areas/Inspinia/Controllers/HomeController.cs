using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Areas.Inspinia.Controllers
{
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Json("Selam");
        }
    }
}
