using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using MiniProject319.Services;

namespace MiniProject319.Controllers
{
    public class DataController : Controller
    {
        private DataService dataService;
        private int IdUser = 1;

        public DataController(DataService _dataService)
        {
            this.dataService = _dataService;
        }

        public async Task<IActionResult> Index(int id)
        {
            VMUser data = await dataService.GetDataById(2);
            return View(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VMUser data = new VMUser();
            data = await dataService.GetDataById(id);
            return PartialView(data);   
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMUser dataparam)
        {
            dataparam.ModifiedBy = dataparam.Id;
            VMResponse respon = await dataService.Edit(dataparam);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return PartialView(dataparam);
        }

    }
}
