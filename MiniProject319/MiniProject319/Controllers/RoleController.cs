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


        public async Task<IActionResult> Index(string sortOrder,
                                               string searchString,
                                               string currentFilter,
                                               int? pageNumber,
                                               int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<VMMrole> data = await roleService.GetAllData();
            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.Name.ToLower().Contains(searchString.ToLower())
                ).ToList();


            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.Name).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.Name).ToList();
                    break;
            }

            return View(PaginatedList<VMMrole>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public async Task<IActionResult> Index_MenuAccess(string sortOrder,
                                              string searchString,
                                              string currentFilter,
                                              int? pageNumber,
                                              int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<VMMrole> data = await roleService.GetAllData();
            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.Name.ToLower().Contains(searchString.ToLower())
                ).ToList();


            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.Name).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.Name).ToList();
                    break;
            }

            return View(PaginatedList<VMMrole>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            MRole data = new MRole();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMMrole dataParam)
        {
            dataParam.CreatedBy = IdUser;

            VMResponse respon = await roleService.Create(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return View(dataParam);
        }

        public async Task<JsonResult> CheckNameIsExist(string roleName, int id)
        {
            bool isExist = await roleService.CheckRoleByName(roleName, id);
            return Json(isExist);
        }


        //public async Task<IActionResult> Edit(int id)
        //{
        //    VMMrole data = await roleService.GetDataById(id);
        //    return View(data);
        //}

        //public async Task<IActionResult> Edit_MenuAccess(int id)
        //{
        //    VMMrole data = await roleService.GetDataById(id);
        //    ViewBag.role_menu = data.role_menu;
        //    return View(data);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(VMMrole dataParam)
        //{
        //    dataParam.UpdateBy = IdUser;

        //    VMResponse respon = await roleService.Edit(dataParam);

        //    if (respon.Success)
        //    {
        //        return Json(new { dataRespon = respon });
        //    }
        //    return View(dataParam);
        //}

        public async Task<IActionResult> Detail(int id)
        {
            VMMrole data = await roleService.GetDataById(id);
            return View(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            VMMrole data = await roleService.GetDataById(id);
            return View(data);
        }
        //[HttpPost]
        //public async Task<IActionResult> SureDelete(int id)
        //{
        //    int createBy = IdUser;
        //    VMResponse respon = await roleService.Delete(id);

        //    if (respon.Success)
        //    {
        //        //return RedirectToAction("Index");
        //        return Json(new { dataRespon = respon });
        //    }
        //    return RedirectToAction("Index");
        //}




    }
}
