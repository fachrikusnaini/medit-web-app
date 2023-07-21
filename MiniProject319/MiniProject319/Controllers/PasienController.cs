using Microsoft.AspNetCore.Mvc;
using MiniProject319.ViewModels;
using MiniProject319.Services;

namespace MiniProject319.Controllers
{
    public class PasienController : Controller
    {
        private PasienService pasienService;

        private int IdUser = 1;

        public PasienController(PasienService _pasienService)
        {
            this.pasienService = _pasienService;
        }


         public async Task<IActionResult> Index(string sortOrder,
                                                    string searchString,
                                                    string currentFilter,
                                                    int? pageNumber,
                                                    int? pageSize)
        {
            ViewBag.Currentsort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<VMPasien> data = await pasienService.GetDataById(1);
            if (!string.IsNullOrEmpty(searchString))
            {

                data = data.Where(a => a.Fullname.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.Fullname).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.Fullname).ToList();
                    break;
            }

            return PartialView(PaginatedList<VMPasien>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }
    }
}
