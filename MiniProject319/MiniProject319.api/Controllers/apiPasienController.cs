using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiPasienController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiPasienController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMMPasien> GetAllData()
        {
            List<VMMPasien> data = (from cm in db.MCustomerMembers
                                   join c in db.MCustomers on cm.CustomerId equals c.Id
                                   join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                                   join bg in db.MBloodGroups on c.BloodGroupId equals bg.Id
                                   join b in db.MBiodata on c.BiodataId equals b.Id
                                   where cm.IsDelete == false
                                   select new VMMPasien
                                   {
                                       Id = cm.Id,
                                       CustomerId = cm.CustomerId,
                                       ParentBiodataId = cm.ParentBiodataId,
                                       CustomerRelationId = cm.CustomerRelationId,

                                       Fullname = b.Fullname,
                                       Dob = c.Dob,
                                       Gender = c.Gender,
                                       BloodGroupId = c.BloodGroupId,
                                       RhesusType = c.RhesusType,
                                       Height = c.Height,
                                       Weight = c.Weight,
                                       Name = cr.Name,

                                       CreatedBy = c.Id
                                   }).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public List<VMMPasien> GetDataById(int id)
        {
            List<VMMPasien> data = (from cm in db.MCustomerMembers
                                   join c in db.MCustomers on cm.CustomerId equals c.Id
                                   join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                                   join bg in db.MBloodGroups on c.BloodGroupId equals bg.Id
                                   join b in db.MBiodata on c.BiodataId equals b.Id
                                   where cm.IsDelete == false && cm.ParentBiodataId == id
                                   select new VMMPasien
                                   {
                                       Id = cm.Id,
                                       CustomerId = cm.CustomerId,
                                       ParentBiodataId = cm.ParentBiodataId,
                                       CustomerRelationId = cm.CustomerRelationId,

                                       Fullname = b.Fullname,
                                       Dob = c.Dob,
                                       Gender = c.Gender,
                                       BloodGroupId = c.BloodGroupId,
                                       RhesusType = c.RhesusType,
                                       Height = c.Height,
                                       Weight = c.Weight,
                                       Name = cr.Name,

                                       CreatedBy = c.Id
                                   }).ToList();
            return data;
        }


        [HttpPost("Save")]
        public VMResponse Save(VMMPasien data)
        {
            data.CreatedBy = IdUser;
            data.CreatedOn = DateTime.Now;
            data.IsDelete = false;

            try
            {
                db.Add(data);
                db.SaveChanges();
                respon.Message = "Saved ya guys";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = "failed save: " + e.Message;
                throw;
            }
            return respon;

        }

        [HttpDelete("Delete/{id}")]
        public VMResponse Delete(int id)

        {
            MCustomerRelation dt = db.MCustomerRelations.Where(a => a.Id == id).FirstOrDefault();
            if (dt != null)
            {

                dt.IsDelete = true;
                dt.DeletedBy = IdUser;
                dt.DeletedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();
                    respon.Message = $"Success Deleted";
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "failed";

                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "DataNotfound";
            }
            return respon;
        }

        [HttpPut("MultipleDelete")]
        public VMResponse MultipleDelete(List<int> listId)
        {
            if (listId.Count > 0)
            {
                foreach (int item in listId)
                {

                    MCustomerRelation dt = db.MCustomerRelations.Where(a => a.Id == item).FirstOrDefault();

                    dt.IsDelete = true;
                    dt.DeletedBy = IdUser;
                    dt.DeletedOn = DateTime.Now;
                    db.Update(dt);
                }
                try
                {
                    db.SaveChanges();

                    respon.Message = "Data success saved";
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "failed";
                    throw;
                }
            }
            else
            {

            }
            return respon;

        }
    }
}
