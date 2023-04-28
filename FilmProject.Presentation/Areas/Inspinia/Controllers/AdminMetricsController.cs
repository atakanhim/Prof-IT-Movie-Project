using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Areas.Inspinia.Controllers
{
    [Area("Inspinia")]
    public class AdminMetricsController : Controller
    {
        public IActionResult Metrics()
        {
            return View();
        }
    }
}
