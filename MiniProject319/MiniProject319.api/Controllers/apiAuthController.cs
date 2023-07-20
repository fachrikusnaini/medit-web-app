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
        private int idUser = 1;
        private VMResponse response = new VMResponse();
        public apiAuthController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMm_user> GetAllData()
        {
            List<VMm_user> data = (from MUser u in db.MUsers
                                        join r in db.MRoles on u.RoleId equals r.Id
                                        join b in db.MBiodata on u.BiodataId equals b.Id
                                        where u.IsDelete == false
                                        select new VMm_user
                                        {
                                            Id = u.Id,
                                            Email = u.Email,
                                            Password = u.Password,

                                            RoleId = u.RoleId,
                                            Name = r.Name,

                                            BiodataId = u.BiodataId,
                                            Fullname = b.Fullname,
                                            MobilePhone = b.MobilePhone,
                                            ImagePath = b.ImagePath
                                        }).ToList();

            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public VMm_user GetDataById(int id)
        {
            VMm_user data = (from MUser u in db.MUsers
                             join r in db.MRoles on u.RoleId equals r.Id
                             join b in db.MBiodata on u.BiodataId equals b.Id
                             join c in db.MCustomers on u.BiodataId equals c.BiodataId
                             join a in db.MAdmins on u.BiodataId equals a.BiodataId
                             join d in db.MDoctors on u.BiodataId equals d.BiodataId
                             where u.IsDelete == false && u.Id == id
                                   select new VMm_user
                                   {
                                       Id = u.Id,
                                       Email = u.Email,
                                       Password = u.Password,

                                       RoleId = u.RoleId,
                                       Name = r.Name,

                                       BiodataId = u.BiodataId,
                                       Fullname = b.Fullname,
                                       MobilePhone = b.MobilePhone,
                                       ImagePath = b.ImagePath,

                                   }).FirstOrDefault()!;

            return data;
        }

        [HttpPost("Register")]
        public VMResponse Register(VMm_user data)
        {
            MRole role = db.MRoles.Where(a => a.Id == data.RoleId).FirstOrDefault()!;
            MBiodatum biodata = new MBiodatum()
            {
               
                Fullname = data.Fullname,
                MobilePhone = data.MobilePhone,
                CreatedBy = idUser,
                CreatedOn = DateTime.Now,
            };
            MUser user = new MUser()
            {
                Email = data.Email,
                Password = data.Password,
                //BiodataId = biodata.Id,
                RoleId = data.RoleId,
                CreatedOn = DateTime.Now,
                IsLocked = true,
                IsDelete = false,
                CreatedBy = idUser

            };
            if (data.RoleId == 1)
            {
                MAdmin admin = new MAdmin()
                {
                    BiodataId = biodata.Id,
                    IsDelete = false,
                    CreatedBy = idUser,
                    CreatedOn = DateTime.Now
                };

                try
                {
                    db.MBiodata.Add(biodata);
                    db.SaveChanges();
                    user.BiodataId = biodata.Id;
                    admin.BiodataId = biodata.Id;
                    db.MUsers.Add(user);
                    db.MAdmins.Add(admin);
                    db.SaveChanges();

                    response.Message = "Data successfully added";
                }
                catch (Exception e)
                {

                    response.Success = false;
                    response.Message = "Failed saved : " + e.Message;
                }
            }
            if (data.RoleId == 2)
            {
                MCustomer costumer = new MCustomer()
                {
                    BiodataId = biodata.Id,
                    IsDelete = false,
                    CreatedBy = idUser,
                    CreatedOn = DateTime.Now
                };

                try
                {
                    db.MBiodata.Add(biodata);
                    db.SaveChanges();
                    user.BiodataId = biodata.Id;
                    costumer.BiodataId = biodata.Id;
                    db.MUsers.Add(user);
                    db.MCustomers.Add(costumer);
                    db.SaveChanges();

                    response.Message = "Data successfully added";
                }
                catch (Exception e)
                {

                    response.Success = false;
                    response.Message = "Failed saved : " + e.Message;
                }
            }
            if (data.RoleId == 3)
            {
                var doctor = new MDoctor()
                {
                    BiodataId = biodata.Id,
                    IsDelete = false,
                    CreatedBy = idUser,
                    CreatedOn = DateTime.Now
                };

                try
                {
                    db.MBiodata.Add(biodata);
                    db.SaveChanges();
                    user.BiodataId = biodata.Id;
                    doctor.BiodataId = biodata.Id;
                    db.MUsers.Add(user);
                    db.MDoctors.Add(doctor);
                    db.SaveChanges();

                    response.Message = "Data successfully added";
                }
                catch (Exception e)
                {

                    response.Success = false;
                    response.Message = "Failed saved : " + e.Message;
                }
            }
            return response;
        }
        [HttpGet("CheckRegisterByEmail/{email}/{id}")]
        public bool CheckName(string email, int id)
        {
            MUser data = new MUser();

            if (id == 0)
            {
                data = db.MUsers.Where(
                    a =>
                    a.IsDelete == false && a.Email == email
                    ).FirstOrDefault()!;
            }
            else
            {
                data = db.MUsers.Where(
                    a =>
                    a.IsDelete == false && a.Id != id && a.Email == email
                    ).FirstOrDefault()!;
            }

            if (data != null)
            {
                return true;
            }

            return false;
        }
    }
}
