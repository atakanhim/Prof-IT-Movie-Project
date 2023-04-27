using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core_2_1.Controllers
{
    [Area("Inspinia")]
    public class MovieController : Controller
    {
        
        public IActionResult GetAddMovieForm() { 
            return View();
        }
    }
}
