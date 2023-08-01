using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class BankController : Controller
    {
        private readonly BankServices bankServices;
        VMResponse response = new VMResponse();

        public BankController(BankServices _bankServices)
        {
            this.bankServices = _bankServices;

        }
        public async Task<IActionResult> Index(
                                        string sortOrder,
                                        string searchString,
                                        string currentFilter,
                                        int? pageNumber,
                                        int? pageSize)
        {

            ViewBag.CurrentSort = sortOrder;
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

            List<MBank> data = await bankServices.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.Name.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.Name).ToList();
                    break;

                default:
                    data = data.OrderBy(a => a.Name).ToList();
                    break;
            }
            return View(PaginatedList<MBank>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create ()
        {
            return PartialView();
        }

        public async Task<JsonResult> CheckNameIsExist(string name, int id)
        {
            bool isExist = await bankServices.CheckNameIsExist(name, id);
            return Json(isExist);
        }

        public async Task<JsonResult> CheckCodeIsExist(string kodeva, int id)
        {
            bool isExist = await bankServices.CheckCodeIsExist(kodeva, id);
            return Json(isExist);
        }

        [HttpPost]
        public async Task <IActionResult> Create(MBank data)
        {
            VMResponse response = await bankServices.Create(data);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            MBank data = await bankServices.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MBank dataParam)
        {
            VMResponse response = await bankServices.Edit(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return View(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MBank data = await bankServices.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MBank dataParam)
        {
            VMResponse response = await bankServices.Delete(((int)dataParam.Id));

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> CheckIsExist(VMBank dataParam)
        {
            VMResponse response = await bankServices.CheckIsExist(dataParam);
            return Json(new { dataResponse = response });
        }
    }
}
