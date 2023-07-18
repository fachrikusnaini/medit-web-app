using Microsoft.AspNetCore.Mvc;

namespace MiniProject319.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return PartialView();
        }
    }
}
