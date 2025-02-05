using Microsoft.AspNetCore.Mvc;

namespace The_Outfit.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Men()
        {
            return View();
        }

        public IActionResult Women()
        {
            return View();
        }

        public IActionResult Sale()
        {
            return View();
        }

    }
}
