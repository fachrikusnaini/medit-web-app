using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class DarahController : Controller
    {
        private DarahService darahService;

        private int IdUser = 1;

        public DarahController(DarahService _darahService)
        {
            this.darahService = _darahService;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString,
            string currentFilter, int? pageNumber, int? pageSize)
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

            List<MBloodGroup> data = await darahService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.Code.ToLower().Contains(searchString.ToLower())
                || a.Description != null && a.Description.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.Code).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.Code).ToList();
                    break;
            }



            return View(PaginatedList<MBloodGroup>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            MBloodGroup data = new MBloodGroup();
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MBloodGroup dataParam)
        {
            dataParam.CreatedBy = IdUser;
            dataParam.CreatedOn = DateTime.Now;
            VMResponse respon = await darahService.Create(dataParam);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon }); //ntar klo udh make json baru dipake
                //return RedirectToAction("Index");
            }
            return PartialView(dataParam);
        }

        public async Task<JsonResult> CheckDarahIsExist(string code, int id)
        {
            bool isExist = await darahService.CheckDarah(code, id);
            return Json(isExist);
        }

            public async Task<IActionResult> Edit(int id)
        {
            MBloodGroup data = await darahService.GetDataById(id);
            return PartialView(data);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MBloodGroup dataParam)
        {
            dataParam.ModifiedBy = IdUser;
            dataParam.ModifiedOn = DateTime.Now;
            VMResponse respon = await darahService.Edit(dataParam);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
                //return RedirectToAction("Index");
            }
            return PartialView(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MBloodGroup data = await darahService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int deletedby = IdUser;
            VMResponse respon = await darahService.Delete(id);

            if (respon.Success)
            {
                return RedirectToAction("Index");
                //return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }

    }
}
