using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class AuthController : Controller
    {
        private AuthService authService;
        VMResponse respon = new VMResponse();

        public AuthController(AuthService _authService)
        {
            this.authService = _authService;
        }
        public IActionResult Login()
        {
            return PartialView();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<JsonResult> LoginSubmit(string email)
        {
            VMTblUsers user = await authService.CheckLogin(email);

            if (user != null)
            {
                respon.Message = $"Hello, {user.Email} welcome to medit";
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Name", user.Name);
                HttpContext.Session.SetInt32("RoleId", user.RoleId == null ? 0 : Convert.ToInt32(user.RoleId));
                HttpContext.Session.SetInt32("BiodataId", user.BiodataId == null ? 0 : Convert.ToInt32(user.BiodataId));


            }
            else
            {
                respon.Success = false;
                respon.Message = $"Ooops, {email} not found, please check your mail";
            }

            return Json(new { dataRespon = respon });
        }
    }
}
