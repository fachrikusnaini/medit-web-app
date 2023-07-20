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

        [HttpGet("CheckLogin/{email}/{password}")]
        public VMMUser CheckLogin(string email, string password)
        {
            VMMUser data = (from u in db.MUsers
                            join r in db.MRoles on u.RoleId equals r.Id
                            where u.Email == email
                            select new VMMUser
                            {
                                Id = u.Id,
                                Email = u.Email,

                                RoleId = r.Id,
                                //NameRole = r.Name,
                            }).FirstOrDefault()!;
            return data;
        }

    }

}
