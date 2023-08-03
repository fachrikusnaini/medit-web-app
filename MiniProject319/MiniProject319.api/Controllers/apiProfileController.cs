using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using System.Configuration;
using System.Net.Mail;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp ;
using MailKit.Security;
using MiniProject319.api.Services.EmailService;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiProfileController : ControllerBase
    {

        private readonly DB_SpecificationContext db;
        private readonly IConfiguration configuration;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiProfileController(DB_SpecificationContext _db, IConfiguration _configuration )
        {
            this.db = _db;
            this.configuration = _configuration;

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

        [HttpPost("EditEmail")]
        public VMResponse EditEmail(VMUser data)
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
                CreatedBy = IdUser
            };



            TToken token = new TToken()
            {
                Token = randomNumber.ToString(),
                ExpiredOn = currentTime.AddSeconds(30),
                CreatedOn = DateTime.Now,
                IsExpired = false,
                IsDelete = false,
                CreatedBy = IdUser
            };

            var emailVerif = new MimeMessage();
            emailVerif.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailUsername").Value));
            emailVerif.To.Add(MailboxAddress.Parse(user?.Email));
            emailVerif.Subject = "Register User";
            emailVerif.Body = new TextPart(TextFormat.Html)
            {
                Text = @"This your Code OTP : " + token.Token
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
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



                respon.Message = "Data successfully added";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = "Failed saved : " + e.Message;
            }
            return respon;
        }

        //[HttpPost("CheckEmail/{email}")]
        //public VMResponse CheckEmail(string email)
        //{
        //    MUser data = new MUser();

        //    data = db.MUsers.Where(a => a.IsDelete == false && a.Email == email).FirstOrDefault()!;


        //    if (data == null)
        //    {
        //        respon.Message = "Email tidak terdaftar";
        //        respon.Success = false;
        //    }
        //    return respon;
        //}

        [HttpGet("CheckEmail/{email}/{id}")]
        public bool CheckEmail(string email, int id)
        {

            MUser data = new MUser();
            if (id == 0)
            {
                data = db.MUsers.Where(a => a.Email == email && a.IsDelete == false).FirstOrDefault();
            }
            else
            {
                data = db.MUsers.Where(a => a.Email == email && a.IsDelete == false && a.Id != id).FirstOrDefault();

            }
            if (data != null)
            {
                return true;
            }
            return false;

        }

        [HttpPost("CheckOTP/{token}")]
        public VMResponse CheckOTP(string token)
        {
            TToken data = new TToken();
            DateTime date2 = DateTime.Now;

            data = db.TTokens.Where(a => a.IsDelete == false && a.Token == token).FirstOrDefault()!;


            if (data == null)
            {
                respon.Message = "Invalid Code OTP";
                respon.Success = false;
            }
            else if (data.ExpiredOn < date2)
            {
                respon.Message = "Expired Code OTP";
                respon.Success = false;

            }
            //data.IsExpired = true;
            //db.Update(data);
            //db.SaveChanges();

            return respon;
        }

        //try
        //{

        //    MUser mUser = new MUser();
        //    mUser.Email = data.Email;
        //    mUser.ModifiedBy = IdUser;
        //    mUser.ModifiedOn = DateTime.Now;
        //    mUser.IsLocked = true;
        //    mUser.IsDelete = false;



        //    db.Update(mUser);
        //    db.SaveChanges();

        //    TToken tToken = new TToken();
        //    tToken.Email = data.Email;
        //    tToken.UserId = data.Id;
        //    tToken.Token = randomNumber.ToString();
        //    tToken.ExpiredOn = currentTime.AddSeconds(30);
        //    tToken.IsExpired = false;
        //    tToken.CreatedBy = IdUser;
        //    tToken.CreatedOn = DateTime.Now;
        //    tToken.IsDelete = false;

        //    db.Add(tToken);
        //    db.SaveChanges();



        //    respon.Message = "Data Saved";
        //}
        //catch (Exception e)
        //{
        //    respon.Success = false;
        //    respon.Message = "failed save: " + e.Message;
        //}
        //return respon;

        [HttpGet("CheckByPassword/{password}/{id}")]
        public bool CheckByPassword(string password, int id)
        {
            MUser data = new MUser();

            if (id == 0)
            {
                data = db.MUsers.FirstOrDefault(a => a.Password == password && !a.IsDelete);
            }
            else
            {
                data = db.MUsers.FirstOrDefault(a => a.Password == password && !a.IsDelete && a.Id == id);
            }

            return data != null;
        }
        [HttpPut("SureEditP")]
        public VMResponse SureEditP(MUser data)
        {
            MUser user = db.MUsers.Where(a => a.Id == data.Id).FirstOrDefault();

            if (user != null)
            {
                string oldPassword = user.Password;

                user.Password = data.Password;
                user.ModifiedBy = data.Id;
                user.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(user);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "Failed to update password in MUser table.";
                    return respon;
                }

                TResetPassword reset = new TResetPassword
                {
                    OldPassword = oldPassword,
                    NewPassword = data.Password,
                    ResetFor = "Edit Password",
                    CreatedBy = data.Id,
                    CreatedOn = DateTime.Now,
                    IsDelete = false
                };

                try
                {
                    db.TResetPasswords.Add(reset);
                    db.SaveChanges();

                    respon.Message = "Password updated successfully, and password reset history recorded.";
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "Failed to record password reset history in TResetPassword table.";
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "User not found in MUser table.";
            }

            return respon;
        }

        [HttpGet("CheckPassword/{Password}/{id}")]
        public bool CheckPassword(string password, int id)
        {

            MUser data = new MUser();
            if (id == 0)
            {
                data = db.MUsers.Where(a => a.Password == password && a.IsDelete == false).FirstOrDefault()!;
            }
            else
            {
                data = db.MUsers.Where(a => a.Password == password && a.IsDelete == false && a.Id != id).FirstOrDefault()!;

            }
            if (data != null)
            {
                return true;
            }
            return false;

        }


        //INI Belum dipakai
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


