using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiProfileController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiProfileController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMUser> GetAllData()
        {

            List<VMUser> data = (from u in db.MUsers
                                 join b in db.MBiodata on u.BiodataId equals b.Id
                                 join c in db.MCustomers on b.Id equals c.BiodataId
                                 join r in db.MRoles on u.RoleId equals r.Id
                                 where u.IsDelete == false
                                 select new VMUser
                                 {
                                     Id = u.Id,
                                     BiodataId = u.BiodataId,
                                     RoleId = r.Id,
                                     CustomerId = c.Id,

                                     Fullname = b.Fullname,
                                     Dob = c.Dob,
                                     MobilePhone = b.MobilePhone,

                                     Email = u.Email,
                                     Password = u.Password,

                                     CreatedOn = u.CreatedOn,
                                     IsDelete = u.IsDelete

                                 }).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public VMUser GetDayaById(int id)
        {

            VMUser data = (from u in db.MUsers
                           join b in db.MBiodata on u.BiodataId equals b.Id
                           join c in db.MCustomers on b.Id equals c.BiodataId
                           join r in db.MRoles on u.RoleId equals r.Id
                           where u.IsDelete == false && u.Id == id
                           select new VMUser
                           {
                               Id = u.Id,
                               BiodataId = u.BiodataId,
                               RoleId = r.Id,
                               CustomerId = c.Id,


                               Fullname = b.Fullname,
                               Dob = c.Dob,
                               MobilePhone = b.MobilePhone,

                               Email = u.Email,
                               Password = u.Password,

                               CreatedOn = u.CreatedOn


                           }).FirstOrDefault()!;
            return data;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(VMUser data)
        {
            MBiodata dt = db.MBiodata.Where(a => a.Id == data.BiodataId).FirstOrDefault()!;
            if (dt != null)
            {
                try
                {
                    dt.Fullname = data.Fullname;
                    dt.MobilePhone = data.MobilePhone;
                    dt.ModifiedBy = data.Id;
                    dt.ModifiedOn = DateTime.Now;
                    db.Update(dt);

                    MCustomer dc = db.MCustomers.Where(b => b.Id == data.CustomerId).FirstOrDefault()!;
                    dc.Dob = data.Dob;
                    dc.ModifiedBy = data.Id;
                    dc.ModifiedOn = DateTime.Now;
                    db.Update(dc);

                    db.SaveChanges();
                    respon.Message = "Edit BErhasil";
                }
                catch (Exception e)
                {

                    respon.Success = false;
                    respon.Message = "Failed Edit : " + e.Message;

                }
            }

            else
            {
                respon.Success = false;
                respon.Message = "Data Tidak Ada";
            }

            return respon;
        }

        [HttpPut("EditEmail")]
        public VMResponse EditEmail(VMUser data)
        {
            MUser du = db.MUsers.Where(a => a.Id == data.Id).FirstOrDefault()!;
            if (du != null)
            {
                du.Email = data.Email;
                du.ModifiedBy = data.Id;
                du.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(du);
                    db.SaveChanges();
                    respon.Message = "Email Updated";

                }
                catch (Exception e)
                {
                    respon.Success= false;
                    respon.Message = "Update Failed" + e.Message;
                    
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Email Tidak Ada";
            }
            return respon;
        }

        [HttpPut("EditPass")]
        public VMResponse EditPass(VMUser data)
        {
            MUser du = db.MUsers.Where(a => a.Id == data.Id).FirstOrDefault()!;
            if (du != null)
            {
                du.Password = data.Password;
                du.ModifiedBy = data.Id;
                du.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(du);
                    db.SaveChanges();
                    respon.Message = "Password Updated";

                }
                catch (Exception e)
                {
                    respon.Success = false;
                    respon.Message = "Update Failed" + e.Message;

                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Password Tidak Ada";
            }
            return respon;
        }

    }
}


