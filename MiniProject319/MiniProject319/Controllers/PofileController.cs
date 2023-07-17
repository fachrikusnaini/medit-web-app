using Microsoft.AspNetCore.Mvc;

namespace MiniProject319.Controllers
{
    public class PofileController : Controller
    {
        public IActionResult Index()
        {
            return PartialView();
        }
    }
}
