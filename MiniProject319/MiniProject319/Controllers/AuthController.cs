using Microsoft.AspNetCore.Mvc;
using MiniProject319.api.Services.EmailService;
using MiniProject319.DataModels;
using MiniProject319.Models;
using MiniProject319.Services;
using MiniProject319.ViewModels;
using System.Diagnostics;

namespace MiniProject319.Controllers
{
    public class AuthController : Controller
    {
        private readonly RoleServices roleServices;
        private readonly AuthServices authServices;
        VMResponse response = new VMResponse();

        public AuthController(RoleServices _roleServices, AuthServices _authServices)
        {
            this.roleServices = _roleServices;
            this.authServices = _authServices;
   
        }
        public IActionResult Login()
        {
            return PartialView();
        }

        public async Task<JsonResult> CheckEmailIsExist(string email, int id)
        {
            bool isExist = await authServices.CheckRegisterByEmail(email, id);
            return Json(isExist);
        }

        public async Task<IActionResult> Register()
        {
            VMm_user data = new VMm_user();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Register(VMm_user dataParam)
        {
            VMResponse response = await authServices.Register(dataParam);

            if (response.Success)
            {
                HttpContext.Session.SetString("Email", dataParam.Email);
                return Json(new { dataResponse = response });
            }
            return View(dataParam);

        }

        public async Task<IActionResult> Verification()
        {
            VMm_user data = new VMm_user();
            return View(data);
        }

        public async Task<IActionResult> SetPassword()
        {
            VMm_user data = new VMm_user();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> SetPassword(VMm_user dataParam)
        {
            dataParam.Email = HttpContext.Session.GetString("Email");
            VMResponse response = await authServices.SetPassword(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return View(dataParam);

        }

        public async Task<IActionResult> Biodata()
        {
            VMm_user data = new VMm_user();
            List<MRole> listRole = await roleServices.GetAllData();
            ViewBag.ListRole = listRole;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Biodata(VMm_user dataParam)
        {
            dataParam.Email = HttpContext.Session.GetString("Email");
            VMResponse response = await authServices.Biodata(dataParam);

            if (response.Success)
            {
                HttpContext.Session.Remove(dataParam.Email);
                return Json(new { dataResponse = response });
            }
            return View(dataParam);

        }

        public async Task<JsonResult> CheckOTP(string token)
        {
            bool isExist = await authServices.CheckOTP(token);
            return Json(isExist);
        }

    }
}
