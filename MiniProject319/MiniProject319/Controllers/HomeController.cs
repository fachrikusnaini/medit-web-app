using Microsoft.AspNetCore.Mvc;
using MiniProject319.Models;
using MiniProject319.Services;
using MiniProject319.ViewModels;
using System.Diagnostics;

namespace MiniProject319.Controllers
{
    public class HomeController : Controller
    {
        private MenuService menuService;

        public HomeController(MenuService _menuService)
        {
            this.menuService = _menuService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}