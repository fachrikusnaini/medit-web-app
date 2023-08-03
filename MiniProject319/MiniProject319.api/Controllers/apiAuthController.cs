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
using System;
using System.Security.Cryptography.X509Certificates;

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
                ExpiredOn = currentTime.AddSeconds(30),
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

        [HttpPost("CheckOTP/{token}")]
        public VMResponse CheckOTP(string token)
        {
            TToken data = new TToken();
            DateTime date2 = DateTime.Now;

            data = db.TTokens.Where(a => a.IsDelete == false && a.Token == token && a.IsExpired == false).FirstOrDefault()!;


            if (data == null)
            {
                response.Message = "Kode OTP salah";
                response.Success = false;
            }
            else if (data.ExpiredOn < date2)
            {
                response.Message = "Kode OTP sudah kadaluarsa";
                response.Success = false;

            } else if (data.IsExpired == true)
            {
                response.Message = "Kode OTP sudah Expired";
                response.Success = false;
            }
            //data.IsExpired = true;
            //db.Update(data);
            //db.SaveChanges();

            return response;
        }

        [HttpPost("CheckEmail/{email}")]
        public VMResponse CheckEmail(string email)
        {
            MUser data = new MUser();

            data = db.MUsers.Where(a => a.IsDelete == false && a.Email == email && a.IsLocked == false).FirstOrDefault()!;


            if (data == null)
            {
                response.Message = "Email tidak terdaftar";
                response.Success = false;
            }
            return response;
        }

        [HttpPost("ForgotPassword")]
        public VMResponse ForgotPassword(VMm_user dataParam)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            DateTime currentTime = DateTime.Now;

            MUser userResult = db.MUsers.Where(a => a.IsDelete == false && a.Email == dataParam.Email && a.IsLocked == false).FirstOrDefault()!;
            userResult.ModifiedBy = idUser;
            userResult.ModifiedOn = DateTime.Now;
            

            TToken tokenResult = db.TTokens.Where(a => a.IsDelete == false && a.Email == dataParam.Email).FirstOrDefault()!;
            tokenResult.Token = randomNumber.ToString();
            tokenResult.ExpiredOn = currentTime.AddSeconds(30);
            tokenResult.IsExpired = false;
            tokenResult.UsedFor = "Forgot Password";
            tokenResult.CreatedBy = idUser;
            tokenResult.CreatedOn = DateTime.Now;
            tokenResult.ModifiedBy = idUser;
            tokenResult.ModifiedOn = DateTime.Now;
            tokenResult.IsDelete = false;


            var emailVerif = new MimeMessage();
            emailVerif.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailUsername").Value));
            emailVerif.To.Add(MailboxAddress.Parse(userResult.Email));
            emailVerif.Subject = "Register User";
            emailVerif.Body = new TextPart(TextFormat.Html)
            {
                Text = @"This your Code OTP : " + tokenResult.Token
            };

            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetSection("EmailUsername").Value, configuration.GetSection("EmailPassword").Value);
            smtp.Send(emailVerif);
            smtp.Disconnect(true);


            try
            {
                db.MUsers.Update(userResult);
                db.SaveChanges();
                db.TTokens.Update(tokenResult);
                db.SaveChanges();

                response.Message = "Data successfully Updated";
                response.Entity = userResult;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Failed saved : " + e.Message;
            }
            return response;
        }

        [HttpPost("SetPassword_ForgotPassword")]
        public VMResponse SetPassword_ForgotPassword (VMm_user data)
        {

            TResetPassword dataResetPassword = new TResetPassword();
            MUser user = db.MUsers.Where(a => a.IsDelete == false && a.Email == data.Email && a.IsLocked == false).FirstOrDefault()!;
            
            dataResetPassword.OldPassword = user.Password;
            dataResetPassword.NewPassword = data.Password;
            dataResetPassword.ResetFor = "Forgot Password";
            dataResetPassword.CreatedBy = idUser;
            dataResetPassword.CreatedOn = DateTime.Now;
            dataResetPassword.IsDelete = false;

            db.TResetPasswords.Add(dataResetPassword);
            db.SaveChanges();

            user.Password = data.Password;
            try
            {

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
        [HttpPost("ResendOTP")]
        public VMResponse ResendOTP(VMm_user dataParam)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            DateTime currentTime = DateTime.Now;

            MUser userResult = db.MUsers.Where(a => a.IsDelete == false && a.Email == dataParam.Email && a.IsLocked == false).FirstOrDefault()!;


            TToken tokenResult = db.TTokens.Where(a => a.IsDelete == false && a.Email == dataParam.Email).FirstOrDefault()!;
            tokenResult.IsExpired = true;

            db.TTokens.Update(tokenResult);
            db.SaveChanges();

            TToken dataTokenNew = new TToken();

            dataTokenNew.Email = userResult.Email;
            dataTokenNew.UserId = userResult.Id;
            dataTokenNew.Token = randomNumber.ToString();
            dataTokenNew.ExpiredOn = currentTime.AddSeconds(30);
            dataTokenNew.IsExpired = false;
            dataTokenNew.CreatedOn = DateTime.Now;
            dataTokenNew.CreatedBy = idUser;
            dataTokenNew.ModifiedBy = idUser;
            dataTokenNew.ModifiedOn = DateTime.Now;
            dataTokenNew.IsDelete = false;


            var emailVerif = new MimeMessage();
            emailVerif.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailUsername").Value));
            emailVerif.To.Add(MailboxAddress.Parse(userResult.Email));
            emailVerif.Subject = "Register User";
            emailVerif.Body = new TextPart(TextFormat.Html)
            {
                Text = @"This your Resend Code OTP : " + dataTokenNew.Token
            };

            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetSection("EmailUsername").Value, configuration.GetSection("EmailPassword").Value);
            smtp.Send(emailVerif);
            smtp.Disconnect(true);


            try
            {
                db.TTokens.Add(dataTokenNew);
                db.SaveChanges();

                response.Message = "Check your email for code OTP";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Failed saved : " + e.Message;
            }
            return response;
        }

        [HttpPost("ResendOTPDaftar")]
        public VMResponse ResendOTPDaftar(VMm_user dataParam)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            DateTime currentTime = DateTime.Now;

            MUser userResult = db.MUsers.Where(a => a.IsDelete == false && a.Email == dataParam.Email).FirstOrDefault()!;


            TToken tokenResult = db.TTokens.Where(a => a.IsDelete == false && a.Email == dataParam.Email).FirstOrDefault()!;
            tokenResult.IsExpired = true;

            db.TTokens.Update(tokenResult);
            db.SaveChanges();

            TToken tokenNew = new TToken();
            tokenNew.Email = userResult.Email;
            tokenNew.UserId = userResult.Id;
            tokenNew.Token = randomNumber.ToString();
            tokenNew.ExpiredOn = currentTime.AddSeconds(30);
            tokenNew.IsExpired = false;
            tokenNew.ModifiedBy = idUser;
            tokenNew.ModifiedOn = DateTime.Now;
            tokenNew.CreatedOn = DateTime.Now;
            tokenNew.CreatedBy = idUser;
            tokenNew.IsDelete = false;




            var emailVerif = new MimeMessage();
            emailVerif.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailUsername").Value));
            emailVerif.To.Add(MailboxAddress.Parse(userResult.Email));
            emailVerif.Subject = "Register User";
            emailVerif.Body = new TextPart(TextFormat.Html)
            {
                Text = @"This your Resend Code OTP : " + tokenNew.Token
            };

            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetSection("EmailUsername").Value, configuration.GetSection("EmailPassword").Value);
            smtp.Send(emailVerif);
            smtp.Disconnect(true);


            try
            {
                db.TTokens.Add(tokenNew);
                db.SaveChanges();

                response.Message = "Check your email for code OTP";
                //response.Entity = userResult;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "Failed saved : " + e.Message;
            }
            return response;
        }
    }
}
