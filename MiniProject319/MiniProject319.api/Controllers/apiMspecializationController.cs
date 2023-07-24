using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319;
using MiniProject319.DataModels;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;


namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiMspecializationController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private VMResponse respon = new VMResponse();
        
        private int IdUser = 1;

        public apiMspecializationController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<MSpecialization> GetAllData()
        {
            List<MSpecialization> data = db.MSpecializations.Where(a => a.IsDelete == false).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public MSpecialization DataById(int id)
        {
            MSpecialization result = db.MSpecializations.Where(a => a.Id == id).FirstOrDefault()!;
            return result;

        }

        [HttpGet("CheckMSpecializationByName/{name}/{id}")]
        public bool CheckName(string name, int id)
        {
            MSpecialization data = new MSpecialization();
            if (id == 0)
            {
                data = db.MSpecializations.Where(a => a.Name == name && a.IsDelete == false).FirstOrDefault()!;
            }
            else
            {
                data = db.MSpecializations.Where(a => a.Name == name && a.IsDelete == false && a.Id != id).FirstOrDefault()!;

            }
            if (data != null)
            {
                return true;
            }
            return false;


        }

        [HttpPost("Save")]
        public VMResponse Save(MSpecialization data)
        {
         
            data.CreatedBy = IdUser;
            data.CreatedOn = DateTime.Now;
            data.ModifiedBy = IdUser;
            data.ModifiedOn = DateTime.Now;
            data.IsDelete = false;

            try
            {
                db.Add(data);
                db.SaveChanges();

                respon.Message = "Data Success Saved";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = " Failed Saved : " + e.Message;
            }
            return respon;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(MSpecialization data)
        {
            MSpecialization dt = db.MSpecializations.Where(a => a.Id == data.Id).FirstOrDefault()!;
            if (dt != null)
            {

                dt.Name = data.Name;
                
                dt.ModifiedBy = IdUser;
                dt.ModifiedOn = DateTime.Now;
                

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = "Berhasil Di ubah";
                }
                catch (Exception e)
                {

                    respon.Success = false;
                    respon.Message = "Gagal Di Ubah" + e.Message;
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
            MSpecialization dt = db.MSpecializations.Where(a => a.Id == id).FirstOrDefault()!;
            if (dt != null)
            {
                dt.IsDelete = true;
                dt.ModifiedBy = IdUser;
                dt.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = $"{dt.Name} Data Success Delete";
                }
                catch (Exception e)
                {
                    respon.Success = false;
                    respon.Message = "Delete Failed  : " + e.Message;
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = " Data not found : ";
            }
            return respon;
        }
    }
}
