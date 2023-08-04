using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiBankController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private readonly IConfiguration configuration;
        private int idUser = 1;
        private VMResponse response = new VMResponse();
        public apiBankController(DB_SpecificationContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }

        [HttpGet("GetAllData")]
        public List<MBank> GetAllData()
        {
            List<MBank> data = (from MBank b in db.MBanks
                                   where b.IsDelete == false
                                   select new MBank
                                   {
                                       Id = b.Id,
                                       Name = b.Name,
                                       VaCode = b.VaCode
                                   }).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public MBank GetDataById(int id)
        {
            MBank data = (from MBank b in db.MBanks
                                where b.IsDelete == false && b.Id == id
                                select new MBank
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    VaCode = b.VaCode
                                }).FirstOrDefault()!;
            return data;
        }

        [HttpPost("Save")]
        public VMResponse Save(MBank data)
        {

            MBank bank = new MBank();

            bank.Name = data.Name;
            bank.VaCode = data.VaCode;
            bank.CreatedBy = idUser;
            bank.CreatedOn = DateTime.Now;
            bank.IsDelete = false;

            try
            {
                db.MBanks.Add(bank);
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

        [HttpGet("CheckNameIsExist/{name}/{id}")]
        public bool CheckNameIsExist(string name, int id)
        {
            MBank data = new MBank();

            if (id == 0)
            {
                data = db.MBanks.Where(
                    a =>
                    a.Name == name && a.IsDelete == false
                    ).FirstOrDefault()!;
            }
            else
            {
                data = db.MBanks.Where(
                    a =>
                    a.Name == name && a.IsDelete == false && a.Id != id
                    ).FirstOrDefault()!;
            }

            if (data != null)
            {
                return true;
            }

            return false;
        }

        [HttpGet("CheckCodeIsExist/{kodeva}/{id}")]
        public bool CheckCodeIsExist(string kodeva, int id)
        {
            MBank data = new MBank();

            if (id == 0)
            {
                data = db.MBanks.Where(
                    a =>
                    a.VaCode == kodeva && a.IsDelete == false
                    ).FirstOrDefault()!;
            }
            else
            {
                data = db.MBanks.Where(
                    a =>
                    a.VaCode == kodeva && a.IsDelete == false && a.Id != id
                    ).FirstOrDefault()!;
            }

            if (data != null)
            {
                return true;
            }

            return false;
        }

        [HttpPost("Edit")]
        public VMResponse Edit(MBank data)
        {
            MBank dt = db.MBanks.Where(a => a.Id == data.Id).FirstOrDefault()!;

            if (dt != null)
            {

                dt.Name = data.Name;
                dt.VaCode = data.VaCode;
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
            MBank dt = db.MBanks.Where(a => a.Id == id).FirstOrDefault()!;

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

        [HttpPost("CheckIsExist")]
        public VMResponse CheckIsExist(VMBank dataParam)
        {

            List<MBank> data = db.MBanks.Where(
                a => a.IsDelete == false &&
                a.Name == dataParam.Name ||
                a.VaCode == dataParam.VaCode
                ).ToList()!;

            foreach (var database in data)
            {
                if (dataParam.Id == 0)
                {
                    if (database.Name == dataParam.Name && database.VaCode == dataParam.VaCode && database.IsDelete == false)
                    {
                        response.Message = "Kode VA dan Nama Bank sudah ada";
                        response.Success = false;
                    }
                    else if (database.Name == dataParam.Name && database.IsDelete == false)
                    {
                        response.Message = "Nama bank sudah ada";
                        response.Success = false;
                    }
                    else if (database.VaCode == dataParam.VaCode && database.IsDelete == false)
                    {
                        response.Message = "Kode VA sudah ada";
                        response.Success = false;
                    }
                }
                else if (database.Id != dataParam.Id)
                {
                    if (database.Name == dataParam.Name && database.VaCode == dataParam.VaCode && database.IsDelete == false)
                    {
                        response.Message = "Nama bank dan Kode VA sudah ada";
                        response.Success = false;
                    }
                    else if (database.Name == dataParam.Name && database.IsDelete == false)
                    {
                        response.Message = "Nama bank sudah ada";
                        response.Success = false;
                    }
                    else if (database.VaCode == dataParam.VaCode && database.IsDelete == false)
                    {
                        response.Message = "Kode VA sudah ada";
                        response.Success = false;
                    }
                }
            }
            return response;
        }
    }
}
