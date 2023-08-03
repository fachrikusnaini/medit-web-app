using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiPaymentController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private readonly IConfiguration configuration;
        private int idUser = 1;
        private VMResponse response = new VMResponse();
        public apiPaymentController(DB_SpecificationContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }

        [HttpGet("GetAllData")]
        public List<MPaymentMethod> GetAllData()
        {
            List<MPaymentMethod> data = (from MPaymentMethod b in db.MPaymentMethods
                                        where b.IsDelete == false
                                        select new MPaymentMethod
                                        {
                                            Id = b.Id,
                                            Name = b.Name
                                        }).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public MPaymentMethod GetDataById(int id)
        {
            MPaymentMethod data = (from MPaymentMethod b in db.MPaymentMethods
                                   where b.IsDelete == false && b.Id == id
                          select new MPaymentMethod
                          {
                              Id = b.Id,
                              Name = b.Name
                          }).FirstOrDefault()!;
            return data;
        }

        [HttpPost("Save")]
        public VMResponse Save(MPaymentMethod data)
        {

            MPaymentMethod payment = new MPaymentMethod();

            payment.Name = data.Name;
            payment.CreatedBy = idUser;
            payment.CreatedOn = DateTime.Now;
            payment.IsDelete = false;

            try
            {
                db.MPaymentMethods.Add(payment);
                db.SaveChanges();

                response.Message = "Data Successfully added";
                response.Success = true;
            }
            catch (Exception e)
            {

                response.Message = "Failed Save : " + e.Message;
            }
            return response;
        }

        [HttpPost("CheckNameIsExist")]
        public VMResponse CheckNameIsExist(VMPayment dataParam)
        {

            if (dataParam.Id == 0)
            {
                List<MPaymentMethod> data = db.MPaymentMethods.Where(
                    a => a.IsDelete == false &&
                    a.Name == dataParam.Name
                    ).ToList()!;

                foreach (var database in data)
                {
                    if (database.Name == dataParam.Name)
                    {
                        response.Message = "Nama Pembayaran sudah ada";
                        response.Success = false;
                    }
                }
            }
            else
            {
                MPaymentMethod data = db.MPaymentMethods.Where(
                    a => a.IsDelete == false &&
                    a.Name == dataParam.Name
                    ).FirstOrDefault()!;

                if (data.Id != dataParam.Id)
                {
                    if (data.Name == dataParam.Name && data.Id == dataParam.Id)
                    {
                        return response;
                    }
                    else if (data.Name == dataParam.Name)
                    {
                        response.Message = "Nama pembayaran sudah ada";
                        response.Success = false;
                    }
                }
            }
            return response;
        }

        [HttpPost("Edit")]
        public VMResponse Edit(MPaymentMethod data)
        {
            MPaymentMethod dt = db.MPaymentMethods.Where(a => a.Id == data.Id).FirstOrDefault()!;

            if (dt != null)
            {

                dt.Name = data.Name;
                dt.ModifiedBy = idUser;
                dt.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Success = true;
                    response.Message = "Data successfully updated";
                }
                catch (Exception e)
                {

                    response.Success = false;
                    response.Message = "Failed update : " + e.Message;
                }

            }
            else
            {
                response.Success = false;
                response.Message = "Data not found.";
            }
            return response;
        }
        [HttpDelete("Delete/{id}")]

        public VMResponse Delete(int id)
        {
            MPaymentMethod dt = db.MPaymentMethods.Where(a => a.Id == id).FirstOrDefault()!;

            if (dt != null)
            {
                dt.IsDelete = true;
                dt.DeletedBy = idUser;
                dt.DeletedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Success = true;
                    response.Message = "Data successfully deleted";
                }
                catch (Exception e)
                {

                    response.Success = false;
                    response.Message = "Failed delete : " + e.Message;
                }

            }
            else
            {
                response.Success = false;
                response.Message = "Data not found.";
            }
            return response;
        }
    }
}
