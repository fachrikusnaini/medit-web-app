using Microsoft.AspNetCore.Mvc;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuService menuService;

        public MenuController(MenuService _menuService)
        {
            this.menuService = _menuService;
        }
        public async Task<IActionResult> Index()
        {
            List<VMListMenu> data = await menuService.GetListMenu();
            return PartialView(data);
        }
    }
}
