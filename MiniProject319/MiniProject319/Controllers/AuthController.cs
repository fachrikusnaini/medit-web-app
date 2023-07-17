using Microsoft.AspNetCore.Mvc;
using MiniProject319.services;
using MiniProject319.Services;
using MiniProject319.viewmodels;

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
    }
}
