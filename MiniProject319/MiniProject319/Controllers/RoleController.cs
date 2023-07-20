using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class RoleController : Controller
    {
        private RoleService roleService;
        private int IdUser = 1;
        public RoleController(RoleService _roleService)
        {
            this.roleService = _roleService;
        }


        

    }
}
