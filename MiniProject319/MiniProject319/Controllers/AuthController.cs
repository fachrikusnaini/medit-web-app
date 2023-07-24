using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.services;
using MiniProject319.Services;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;


namespace MiniProject319.Controllers
{
    public class AuthController : Controller
    {
        private AuthService authService;
        private RoleService roleService;
        VMResponse respon = new VMResponse();

        public AuthController(AuthService _authService, RoleService _roleService)
        {
            this.authService = _authService;
            this.roleService = _roleService;

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
            VMMUser user = await authService.CheckLogin(email);
            if (user != null)
            {
                respon.Message = $"Hello, {user.Email} Welcome to Medical.IT";
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetInt32("RoleId", Convert.ToInt32(user.RoleId));

            }
            else
            {
                respon.Success = false;
                respon.Message = $"Oops, {email} not found email is wrong, please check it !";
            }
            return Json(new { dataRespon = respon });
        }

        public async Task<IActionResult> Register()
        {
            VMMUser data = new VMMUser();

            List<VMMrole> listRole = await roleService.GetAllData();
            ViewBag.ListRole = listRole;

            return PartialView(data);

        }
    }
}
