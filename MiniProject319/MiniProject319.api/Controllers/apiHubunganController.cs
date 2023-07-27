using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiHubunganController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiHubunganController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<MCustomerRelation> GetAllData()
        {
            List<MCustomerRelation> data = db.MCustomerRelations.Where(a => a.IsDelete == false).ToList();
            return data;
        }


        [HttpGet("GetDataById/{id}")]
        public MCustomerRelation DataById(int id)
        {
            MCustomerRelation result = db.MCustomerRelations.Where(a => a.Id == id).FirstOrDefault();
            return result;
        }

        [HttpPost("Save")]
        public VMResponse Save(MCustomerRelation data)
        {
            data.Name = data.Name ?? "";
            data.CreatedBy = IdUser;
            data.CreatedOn = DateTime.Now;
            data.IsDelete = false;

            try
            {
                db.Add(data);
                db.SaveChanges();
                respon.Message = "Data Successed Saved";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = "Failed to Save";

            }
            return respon;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(MCustomerRelation data)
        {
            MCustomerRelation dt = db.MCustomerRelations.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.Name = data.Name ?? "";
                dt.ModifiedBy = IdUser;
                dt.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();
                    respon.Message = "Success Edited";
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
                respon.Message = "Data Not Found";
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

                    respon.Message = $"Data {dt.Name} Deleted";
                }
                catch (Exception e)
                {
                    respon.Success = false;
                    respon.Message = "Delete Failed + " + e.Message;
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data Not Found";
            }
            return respon;
        }

    }
}
