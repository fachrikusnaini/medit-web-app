using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiMenuController : ControllerBase
    {
        private readonly DB_SpecificationContext db;

        public apiMenuController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetListMenu/{IdRole}")]
        public List<VMListMenu> GetListMenu(int IdRole)
        {
            List<VMListMenu> data = (from a in db.MMenuRoles
                                     join b in db.MMenus on a.MenuId equals b.Id
                                     join c in db.MRoles on a.RoleId equals c.Id
                                     where b.ParentId != 0 && b.IsDelete == false
                                     && a.IsDelete == false && a.RoleId == IdRole
                                     select new VMListMenu
                                     {
                                         MenuId = b.Id,
                                         MenuName = b.Name,

                                         RoleId = c.Id,
                                         RoleName = c.Name,

                                         Parent_Id = b.Id

                                     }).ToList();

            return data;
        }
    }
}
