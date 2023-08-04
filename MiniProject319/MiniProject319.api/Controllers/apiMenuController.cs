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

        [HttpGet("GetPublicMenu")]
        public List<VMListMenu> GetPublicMenu()
        {
            List<VMListMenu> data = (from a in db.MMenuRoles
                                     join b in db.MMenus on a.MenuId equals b.Id
                                     join c in db.MRoles on a.RoleId equals c.Id
                                     where b.IsDelete == false
                                     && a.IsDelete == false && c.IsDelete == false && c.Name == "Public"
                                     select new VMListMenu
                                     {
                                         MenuId = b.Id,
                                         MenuName = b.Name,

                                         RoleId = c.Id,
                                         RoleName = c.Name,


                                     }).ToList();

            return data;
        }

        [HttpGet("GetListMenu/{IdRole}")]
        public List<VMListMenu> GetListMenu(int IdRole)
        {
            List<VMListMenu> data = (from parent in db.MMenus
                                     join a in db.MMenuRoles on parent.Id equals a.MenuId
                                     join b in db.MRoles on a.RoleId equals b.Id
                                     where a.IsDelete == false && b.IsDelete == false && parent.IsDelete == false
                                     && b.Id == IdRole && parent.ParentId == 0
                                     select new VMListMenu
                                     {
                                         MenuId = parent.Id,
                                         MenuName = parent.Name,
                                         
                                         RoleId = b.Id,
                                         RoleName = b.Name,

                                         ListChild = (from child in db.MMenus
                                                      join c in db.MMenuRoles on child.Id equals c.MenuId
                                                      where child.IsDelete == false && c.IsDelete == false
                                                      && child.ParentId == parent.Id
                                                      select new VMListMenu
                                                      {
                                                          MenuId = child.ParentId,
                                                          MenuName = child.Name,

                                                          RoleId = c.RoleId

                                                      }).ToList()


                                     }).ToList();

            return data;
        }

    }
}
