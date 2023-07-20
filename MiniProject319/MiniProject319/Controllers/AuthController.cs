using Microsoft.AspNetCore.Mvc;
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
            List<VMm_role> listRole = await roleServices.GetAllData();
            ViewBag.ListRole = listRole;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Register(VMm_user dataParam)
        {
            VMResponse response = await authServices.Register(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return View(dataParam);

        }
    }
}
