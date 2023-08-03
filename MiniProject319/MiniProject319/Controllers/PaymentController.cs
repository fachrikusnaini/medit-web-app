using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentServices paymentServices;
        VMResponse response = new VMResponse();

        public PaymentController(PaymentServices _paymentServices)
        {
            this.paymentServices = _paymentServices;

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

            List<MPaymentMethod> data = await paymentServices.GetAllData();

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
            return View(PaginatedList<MPaymentMethod>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        public async Task<JsonResult> CheckNameIsExist(VMPayment dataParam)
        {
            VMResponse response = await paymentServices.CheckNameIsExist(dataParam);
            return Json(new { dataResponse = response });
        }

        [HttpPost]
        public async Task<IActionResult> Create(MPaymentMethod data)
        {
            VMResponse response = await paymentServices.Create(data);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            MPaymentMethod data = await paymentServices.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MPaymentMethod dataParam)
        {
            VMResponse response = await paymentServices.Edit(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return View(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MPaymentMethod data = await paymentServices.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MPaymentMethod dataParam)
        {
            VMResponse response = await paymentServices.Delete(((int)dataParam.Id));

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return RedirectToAction("Index");
        }
    }
}
