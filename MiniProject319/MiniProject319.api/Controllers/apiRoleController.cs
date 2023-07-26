using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiRoleController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private int idUser = 1;
        private VMResponse response = new VMResponse();

        public apiRoleController(DB_SpecificationContext _db)
        {
            this.db = _db;

        }

        [HttpGet("GetAllData")]
        public List<MRole> GetAllData()
        {
            List<MRole> data = (from MRole r in db.MRoles
                                   where r.IsDelete == false
                                   select new MRole
                                   {
                                       Id = r.Id,
                                       Name = r.Name,
                                       Code = r.Code

                                   }).ToList();

            return data;
        }
    }
}
