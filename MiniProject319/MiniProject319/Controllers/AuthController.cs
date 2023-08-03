using Microsoft.AspNetCore.Http;
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
            return PartialView(data);
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

        public async Task<IActionResult> Verification(VMm_user dataParam)
        {
            dataParam.Email = HttpContext.Session.GetString("Email");
            return PartialView(dataParam);
        }

        public async Task<IActionResult> SetPassword()
        {
            VMm_user data = new VMm_user();
            return PartialView(data);
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
            return PartialView(data);
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
            VMResponse response = await authServices.CheckOTP(token);
            return Json(new { dataResponse = response });
        }

        public IActionResult Verification_Forgot_Password(VMm_user data)
        {
            //VMm_user data = new VMm_user();
            data.Email = HttpContext.Session.GetString("Email");
            data.IsLocked = HttpContext.Session.GetString("IsLocked") == "True" ? true : false;
            return PartialView(data);
        }
        public IActionResult ForgotPassword()
        {
            VMm_user data = new VMm_user();
            return PartialView(data);
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(VMm_user dataParam)
        {

            HttpContext.Session.SetString("Email", dataParam.Email);
            HttpContext.Session.SetString("IsLocked", dataParam.IsLocked.ToString());
            VMResponse response = await authServices.ForgotPassword(dataParam);

            return Json(new { dataResponse = response });
        }

        public async Task<JsonResult> CheckEmail(string email)
        {
            VMResponse response = await authServices.CheckEmail(email);
            return Json(new { dataResponse = response });
        }
        public IActionResult SetPassword_ForgotPassword(VMm_user dataParam)
        {
            dataParam.Email = HttpContext.Session.GetString("Email");
            dataParam.IsLocked = HttpContext.Session.GetString("IsLocked") == "True" ? true : false;
            return PartialView(dataParam);
        }

        [HttpPost]
        public async Task<IActionResult> Submit_ForgotPassword(VMm_user dataParam)
        {
            VMResponse response = await authServices.SetPassword_ForgotPassword(dataParam);

            if (response.Success)
            {
                //dataParam.Email = HttpContext.Session.GetString("Email");
                //dataParam.IsLocked = HttpContext.Session.GetString("IsLocked") == "True" ? true: false;
                return Json(new { dataResponse = response });
            }
            return View(dataParam);

        }

        public async Task<JsonResult> ResendOTP(VMm_user dataParam)
        {
            VMResponse response = await authServices.ResendOTP(dataParam);
            return Json(new { dataResponse = response });
        }

        public async Task<JsonResult> ResendOTPDaftar(VMm_user dataParam)
        {
            VMResponse response = await authServices.ResendOTPDaftar(dataParam);
            return Json(new { dataResponse = response });
        }
    }
}
