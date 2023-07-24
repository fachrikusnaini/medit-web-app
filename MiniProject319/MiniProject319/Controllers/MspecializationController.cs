using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class MspecializationController : Controller
    {
        private MspecializationService mspecializationService;
        private int IdUser = 1;

        public MspecializationController(MspecializationService _mspecializationService)
        {
            this.mspecializationService = _mspecializationService;
        }

        public async Task<IActionResult> Index(string sortOrder,
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


            List<MSpecialization> data = await mspecializationService.GetAllData();
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

            return View(PaginatedList<MSpecialization>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            MSpecialization data = new MSpecialization();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MSpecialization dataParam)
        {
            dataParam.CreatedBy = IdUser;

            VMResponse respon = await mspecializationService.Create(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return View(dataParam);
        }

        public async Task<JsonResult> CheckNameIsExist(string Name, int id)
        {
            bool isExist = await mspecializationService.CheckMSpecializationByName(Name, id);
            return Json(isExist);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            MSpecialization data = await mspecializationService.GetDataById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MSpecialization dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await mspecializationService.Edit(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return View(dataParam);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            MSpecialization data = await mspecializationService.GetDataById(id);
            return View(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MSpecialization data = await mspecializationService.GetDataById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            
            VMResponse respon = await mspecializationService.Delete(id);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }


    }
}
