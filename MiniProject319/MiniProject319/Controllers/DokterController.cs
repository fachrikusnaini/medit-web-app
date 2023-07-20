using Microsoft.AspNetCore.Mvc;

namespace MiniProject319.Controllers
{
    public class DokterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListDokter()
        {
            return View();
        }
    }
}
