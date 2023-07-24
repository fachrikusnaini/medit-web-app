using Microsoft.AspNetCore.Mvc;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class ProfileController : Controller
    {
        private DataService dataService;
        private int IdUser = 1;

        public ProfileController(DataService _dataService)
        {
            this.dataService = _dataService;
        }

        public async Task<IActionResult> Index(int id)
        {
            VMUser data = await dataService.GetDataById(1);
            return PartialView(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VMUser data = new VMUser();
            data = await dataService.GetDataById(1);
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

        public async Task<IActionResult> EditEmail(int id)
        {
            VMUser data = new VMUser();
            data = await dataService.GetDataById(1);
            return PartialView(data);
        }

        public async Task<IActionResult> EditPass(int id)
        {
            VMUser data = new VMUser();
            data = await dataService.GetDataById(1);
            return PartialView(data);
        }

    }
}
