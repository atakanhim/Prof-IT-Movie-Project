using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Core_2_1.Controllers
{
    [Area("Inspinia")]
    public class LayoutsController : Controller
    {
        public IActionResult Layouts()
        {
            return View();
        }

        public IActionResult OffCanvas()
        {
            return View();
        }
    }
}