using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class HubunganController : Controller
    {

        private HubunganService hubunganService;

        private int IdUser = 1;

        public HubunganController(HubunganService _hubunganService)
        {
            this.hubunganService = _hubunganService;
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

            List<MCustomerRelation> data = await hubunganService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.Name.ToLower().Contains(searchString.ToLower())).ToList();
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



            return View(PaginatedList<MCustomerRelation>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            MCustomerRelation data = new MCustomerRelation();
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MCustomerRelation dataParam)
        {
            dataParam.CreatedBy = IdUser;
            dataParam.CreatedOn = DateTime.Now;
            VMResponse respon = await hubunganService.Create(dataParam);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon }); //ntar klo udh make json baru dipake
                //return RedirectToAction("Index");
            }
            return PartialView(dataParam);
        }

        public async Task<JsonResult> CheckNameIsExist(string name, int id)
        {
            bool isExist = await hubunganService.CheckByName(name, id);
            return Json(isExist);
        }

        public async Task<IActionResult> Edit(int id)
        {
            MCustomerRelation data = await hubunganService.GetDataById(id);
            return PartialView(data);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MCustomerRelation dataParam)
        {
            dataParam.ModifiedBy = IdUser;
            dataParam.ModifiedOn = DateTime.Now;
            VMResponse respon = await hubunganService.Edit(dataParam);
            if (respon.Success)
            {
                //return Json(new { dataRespon = respon });
                return RedirectToAction("Index");
            }
            return View(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MCustomerRelation data = await hubunganService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = IdUser;
            VMResponse respon = await hubunganService.Delete(id);

            if (respon.Success)
            {
                return RedirectToAction("Index");
                //return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }


    }
}
