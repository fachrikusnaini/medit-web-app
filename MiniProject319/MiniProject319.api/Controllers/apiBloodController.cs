using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiBloodController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiBloodController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<MBloodGroup> GetAllData()
        {
            List<MBloodGroup> data = db.MBloodGroups.Where(a => a.IsDelete == false).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public MBloodGroup DataById(int id)
        {
            MBloodGroup result = db.MBloodGroups.Where(a => a.Id == id).FirstOrDefault();
            return result;
        }

        [HttpGet("CheckDarah/{code}/{id}")]
        public bool CheckDarah(string code, int id)
        {

            MBloodGroup data = new MBloodGroup();
            if (id == 0)
            {
                data = db.MBloodGroups.Where(a => a.Code == code && a.IsDelete == false).FirstOrDefault();
            }
            else
            {
                data = db.MBloodGroups.Where(a => a.Code == code && a.IsDelete == false && a.Id != id).FirstOrDefault();

            }
            if (data != null)
            {
                return true;
            }
            return false;

        }

        [HttpPost("Save")]
        public VMResponse Save(MBloodGroup data)
        {
            data.Code = data.Code;
            data.Description = data.Description ;
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
        public VMResponse Edit(MBloodGroup data)
        {
            MBloodGroup dt = db.MBloodGroups.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.Code = data.Code ?? "";
                dt.Description = data.Description;
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
            MBloodGroup dt = db.MBloodGroups.Where(a => a.Id == id).FirstOrDefault();

            if (dt != null)
            {
                dt.IsDelete = true;
                dt.DeletedBy = IdUser;
                dt.DeletedOn = DateTime.Now;
                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = $"Data {dt.Code} Deleted";
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
