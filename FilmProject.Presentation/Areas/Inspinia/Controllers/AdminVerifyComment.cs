using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Areas.Inspinia.Controllers
{
    [Area("Inspinia")]
    public class AdminVerifyComment : Controller
    {
        public IActionResult ListComments()
        {
            return View();
        }
    }
}
