using Microsoft.AspNetCore.Mvc;

namespace MiniProject319.Controllers
{
    public class FaskesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Obat()
        {
            return View();
        }
    }
}
