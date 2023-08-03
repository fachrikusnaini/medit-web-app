using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class ProfileController : Controller
    {
        private DataService dataService;
        private int IdUser = 1;
        int id = 3;

        public ProfileController(DataService _dataService)
        {
            this.dataService = _dataService;
        }

        public async Task<IActionResult> Index(int id)
        {
            VMUser data = await dataService.GetDataById(1);
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
            dataparam.ModifiedOn = DateTime.Now;
            VMResponse respon = await dataService.Edit(dataparam);
            if (respon.Success)
            {
                return RedirectToAction("Index"); //jikalau tidak menggunakan json
               // return Json(new { dataRespon = respon });
            }
            return PartialView(dataparam);
        }

        public async Task<IActionResult> EditEmail(int id)
        {
            VMUser data = new VMUser();
            data = await dataService.GetDataById(id);
            return PartialView(data);
        }

        public async Task<IActionResult> Verification()
        {
            VMUser data = new VMUser();
            return PartialView(data);
        }

        public async Task<JsonResult> CheckOTP(string token)
        {
            VMResponse response = await dataService.CheckOTP(token);
            return Json(new { dataResponse = response });
        }

        [HttpPost]
        public async Task<IActionResult> Register(VMUser dataParam)
        {
            VMResponse response = await dataService.Register(dataParam);

            if (response.Success)
            {
                HttpContext.Session.SetString("Email", dataParam.Email);
                return Json(new { dataResponse = response });
            }
            return View(dataParam);

        }
        

        public async Task<IActionResult> EditPass(int id)
        {
            VMUser data =  await dataService.GetDataById(id);
            return PartialView(data);
        }

        public async Task<IActionResult> SureEditP(int id)
        {
            VMUser data = await dataService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureEditP(MUser dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await dataService.SureEditP(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }
        public async Task<JsonResult> CheckPasswordIsExist(string password, int id)
        {
            bool isExist = await dataService.CheckPassword(password, id);
            return Json(isExist);
        }

        public async Task<JsonResult> CheckEmail(string email, int id)
        {
            //VMResponse response = await dataService.CheckEmail(email, id);
            //return Json(new { dataResponse = response });
            bool isExis = await dataService.CheckEmail(email, id);
            return Json(isExis);
        }

    }
}
