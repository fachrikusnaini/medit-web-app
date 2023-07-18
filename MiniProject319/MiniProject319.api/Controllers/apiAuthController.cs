using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiAuthController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private VMResponse response = new VMResponse();

        public apiAuthController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("CheckLogin/{email}")]
        public VMTblUsers CheckLogin(string email)
        {
            VMTblUsers data = new VMTblUsers();

                        data = (from u in db.MUsers
                                  join r in db.MRoles on u.RoleId equals r.Id
                                  where u.Email == email
                                  select new VMTblUsers
                                  {
                                      Id = u.Id,
                                      Email = u.Email,

                                      RoleId = r.Id,
                                      RoleName = r.Name,

                                  }).FirstOrDefault()!;
            return data;
        }
    }
}
