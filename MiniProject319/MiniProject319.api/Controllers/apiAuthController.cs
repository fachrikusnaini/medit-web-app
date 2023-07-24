using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.ViewModels;
using MiniProject319.DataModels;
using MiniProject319.viewmodels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiAuthController : ControllerBase
    {

        private readonly DB_SpecificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiAuthController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("CheckLogin/{email}")]
        public VMMUser CheckLogin(string email)
        {
            VMMUser data = (from u in db.MUsers
                            join r in db.MRoles on u.RoleId equals r.Id
                            where u.IsDelete == false && u.Email == email 
                            select new VMMUser
                            {
                                Id = u.Id,
                                Email = u.Email,

                                RoleId = r.Id,
                                NameRole = r.Name,
                            }).FirstOrDefault()!;
            return data;
        }

        [HttpGet("MenuAccess/{RoleId}")]
        public List<VMMenu> MenuAccess(int RoleId)
        {
            List<VMMenu> data = (from mmr in db.MMenuRoles
                                 join mm in db.MMenus on mmr.MenuId equals mm.Id
                                 join mr in db.MRoles on mmr.RoleId equals mr.Id
                                 where mm.ParentId != 0 && mm.IsDelete == false
                                 && mmr.IsDelete == false && mmr.RoleId == RoleId
                                 select new VMMenu
                                 {
                                     MenuId = mm.Id,
                                     MenuName = mm.Name,

                                     RoleId = mr.Id,
                                     RoleName = mr.Name,

                                     ParentId = mm.Id

                                 }).ToList();



            return data;
        }
    }
}
