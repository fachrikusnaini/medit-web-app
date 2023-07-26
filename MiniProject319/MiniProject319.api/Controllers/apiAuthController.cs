using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using Org.BouncyCastle.Asn1.Ocsp;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiAuthController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private readonly IConfiguration configuration;
        private int idUser = 1;
        private VMResponse response = new VMResponse();
        public apiAuthController(DB_SpecificationContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
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

        //[HttpPost("Register")]
        //public VMResponse Register(VMm_user data)
        //{
        //    MRole role = db.MRoles.Where(a => a.Id == data.RoleId).FirstOrDefault()!;
        //    MBiodatum biodata = new MBiodatum()
        //    {
               
        //        Fullname = data.Fullname,
        //        MobilePhone = data.MobilePhone,
        //        CreatedBy = idUser,
        //        CreatedOn = DateTime.Now,
        //    };
        //    MUser user = new MUser()
        //    {
        //        Email = data.Email,
        //        Password = data.Password,
        //        //BiodataId = biodata.Id,
        //        RoleId = data.RoleId,
        //        CreatedOn = DateTime.Now,
        //        IsLocked = true,
        //        IsDelete = false,
        //        CreatedBy = idUser

        //    };
        //    if (data.RoleId == 1)
        //    {
        //        MAdmin admin = new MAdmin()
        //        {
        //            BiodataId = biodata.Id,
        //            IsDelete = false,
        //            CreatedBy = idUser,
        //            CreatedOn = DateTime.Now
        //        };

        //        try
        //        {
        //            db.MBiodata.Add(biodata);
        //            db.SaveChanges();
        //            user.BiodataId = biodata.Id;
        //            admin.BiodataId = biodata.Id;
        //            db.MUsers.Add(user);
        //            db.MAdmins.Add(admin);
        //            db.SaveChanges();

        //            response.Message = "Data successfully added";
        //        }
        //        catch (Exception e)
        //        {

        //            response.Success = false;
        //            response.Message = "Failed saved : " + e.Message;
        //        }
        //    }
        //    if (data.RoleId == 2)
        //    {
        //        MCustomer costumer = new MCustomer()
        //        {
        //            BiodataId = biodata.Id,
        //            IsDelete = false,
        //            CreatedBy = idUser,
        //            CreatedOn = DateTime.Now
        //        };

        //        try
        //        {
        //            db.MBiodata.Add(biodata);
        //            db.SaveChanges();
        //            user.BiodataId = biodata.Id;
        //            costumer.BiodataId = biodata.Id;
        //            db.MUsers.Add(user);
        //            db.MCustomers.Add(costumer);
        //            db.SaveChanges();

        //            response.Message = "Data successfully added";
        //        }
        //        catch (Exception e)
        //        {

        //            response.Success = false;
        //            response.Message = "Failed saved : " + e.Message;
        //        }
        //    }
        //    if (data.RoleId == 3)
        //    {
        //        var doctor = new MDoctor()
        //        {
        //            BiodataId = biodata.Id,
        //            IsDelete = false,
        //            CreatedBy = idUser,
        //            CreatedOn = DateTime.Now
        //        };

        //        try
        //        {
        //            db.MBiodata.Add(biodata);
        //            db.SaveChanges();
        //            user.BiodataId = biodata.Id;
        //            doctor.BiodataId = biodata.Id;
        //            db.MUsers.Add(user);
        //            db.MDoctors.Add(doctor);
        //            db.SaveChanges();

        //            response.Message = "Data successfully added";
        //        }
        //        catch (Exception e)
        //        {

        //            response.Success = false;
        //            response.Message = "Failed saved : " + e.Message;
        //        }
        //    }
        //    return response;
        //}
        [HttpGet("CheckRegisterByEmail/{email}/{id}")]
        public bool CheckName(string email, int id)
        {
            MUser data = new MUser();

            if (id == 0)
            {
                data = db.MUsers.Where(
                    a =>
                    a.IsDelete == false && a.Email == email && a.IsLocked == false
                    ).FirstOrDefault()!;
            }
            else
            {
                data = db.MUsers.Where(
                    a =>
                    a.IsDelete == false && a.Id != id && a.Email == email && a.IsLocked == false
                    ).FirstOrDefault()!;
            }

            if (data != null)
            {
                return true;
            }

            return false;
        }

        [HttpGet("GetEmailVerification")]
        public List<VMm_user> GetEmailVerification()
        {
            List<VMm_user> data = (from MUser u in db.MUsers
                                   join t in db.TTokens on u.Id equals t.UserId
                                   where u.IsDelete == false && u.IsLocked == true
                                   select new VMm_user
                                   {
                                       Id = u.Id,
                                       Email = u.Email,
                                       Password = u.Password,

                                       UserId = u.Id,
                                       Token = t.Token,
                                       ExpiredOn = t.ExpiredOn,
                                       IsExpired = t.IsExpired

                                   }).ToList();

            return data;
        }

        [HttpPost("Register")]
        public VMResponse Register(VMm_user data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            DateTime currentTime = DateTime.Now;

            MUser user = new MUser()
            {
                Email = data.Email,
                CreatedOn = DateTime.Now,
                IsLocked = true,
                IsDelete = false,
                CreatedBy = idUser
            };



            TToken token = new TToken()
            {
                Token = randomNumber.ToString(),
                ExpiredOn = currentTime.AddMinutes(10),
                CreatedOn = DateTime.Now,
                IsExpired = false,
                IsDelete = false,
                CreatedBy = idUser

            };

            var emailVerif = new MimeMessage();
            emailVerif.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailUsername").Value));
            emailVerif.To.Add(MailboxAddress.Parse(user?.Email));
            emailVerif.Subject = "Register User";
            emailVerif.Body = new TextPart(TextFormat.Html)
            {
                Text = @"This your Code OTP : " + token.Token
            };

            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetSection("EmailUsername").Value, configuration.GetSection("EmailPassword").Value);
            smtp.Send(emailVerif);
            smtp.Disconnect(true);


            try
            {
                db.MUsers.Add(user);
                db.SaveChanges();
                token.Email = user.Email;
                token.UserId = user.Id;
                db.TTokens.Add(token);
                db.SaveChanges();



                response.Message = "Data successfully added";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Failed saved : " + e.Message;
            }
            return response;
        }

        [HttpPost("SetPassword")]
        public VMResponse SetPassword(VMm_user data)
        {

            try
            {
                MUser user = db.MUsers.Where(a => a.Email == data.Email && a.IsDelete == false).FirstOrDefault()!;

                user.Password = data.Password;

                db.MUsers.Update(user);
                db.SaveChanges();
                response.Message = "Data successfully added";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Failed saved : " + e.Message;
            }
            return response;
        }

        [HttpPost("Biodata")]
        public VMResponse Biodata(VMm_user data)
        {

            MBiodatum biodata = new MBiodatum()
            {
                Fullname = data.Fullname,
                MobilePhone = data.MobilePhone,
                CreatedBy = idUser,
                CreatedOn = DateTime.Now,
            };

            MUser user = db.MUsers.Where(a => a.Email == data.Email && a.IsDelete == false).FirstOrDefault()!;
            user.RoleId = data.RoleId;
            user.IsLocked = false;

            //db.MBiodata.Add(biodata);
            //db.SaveChanges();
            //user.BiodataId = biodata.Id;
            //db.MUsers.Update(user);
            //db.SaveChanges();

            if (data.RoleId == 1)
            {
                MAdmin admin = new MAdmin()
                {
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
                    db.MUsers.Update(user);
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
                MCustomer customer = new MCustomer()
                {
                    IsDelete = false,
                    CreatedBy = idUser,
                    CreatedOn = DateTime.Now
                };

                try
                {
                    db.MBiodata.Add(biodata);
                    db.SaveChanges();
                    user.BiodataId = biodata.Id;
                    customer.BiodataId = biodata.Id;
                    db.MUsers.Update(user);
                    db.MCustomers.Add(customer);
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
                MDoctor doctor = new MDoctor()
                {
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
                    db.MUsers.Update(user);
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

        [HttpGet("CheckOTP/{token}")]
        public bool CheckOTP(string token)
        {
            TToken data = new TToken();

            data = db.TTokens.Where(a =>a.IsDelete == false && a.Token == token).FirstOrDefault()!;
            if (data == null)
            {
                return true;
            }
            return false;
        }
    }
}
