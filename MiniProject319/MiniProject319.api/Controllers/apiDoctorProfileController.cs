﻿using System.Data.SqlTypes;
using System.Drawing;
using System.Text.RegularExpressions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiDoctorProfileController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        int idUserUpdate = 7;
        private VMResponse respon = new VMResponse();

        public apiDoctorProfileController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetCariDokter")]
        public VMCariDokter GetCariDokter()
        {
            VMCariDokter data = (/*from a in db.MDoctors*/
                                 //join b in db.MBiodata on a.BiodataId equals b.Id
                                 //where a.IsDelete == false && b.IsDelete == false
                                new VMCariDokter
                                 {

                                     GetSpecialists = (from a in db.TCurrentDoctorSpecializations
                                                       join b in db.MDoctors on a.DoctorId equals b.Id
                                                       join c in db.MBiodata on b.BiodataId equals c.Id
                                                       join d in db.MSpecializations on a.SpecializationId equals d.Id
                                                       where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false
                                                       select new VMGetSpecialist
                                                       {
                                                           SpecialistId = d.Id,
                                                           SpecialistName = d.Name

                                                       }).ToList(),

                                     GetLocations = (from b in db.MMedicalFacilities 
                                                     join c in db.MMedicalFacilityCategories on b.MedicalFacilityCategoryId equals c.Id
                                                     join d in db.MLocations on b.LocationId equals d.Id
                                                     where b.IsDelete == false && c.IsDelete == false && d.IsDelete == false
                                                     select new VMGetLocation
                                                     {
                                                         LocationId = d.Id,
                                                         LocationName = d.Name

                                                     }).ToList(),

                                     GetTreatments =(from a in db.TDoctorTreatments
                                                     join b in db.MDoctors on a.DoctorId equals b.Id
                                                     where a.IsDelete == false && b.IsDelete == false
                                                     select new VMGetTreatment
                                                     {
                                                         TreatmentId = a.Id,
                                                         TreatmentName = a.Name
                                                         
                                                     }).ToList()

                                 });

            return data;
        }


        [HttpGet("GetAllDataDoctor")]
        public List<VMListDoctor> GetAllDataDoctor()
        {
            List<VMListDoctor> data = (from a in db.TCurrentDoctorSpecializations
                                       join b in db.MDoctors on a.DoctorId equals b.Id
                                       join c in db.MBiodata on b.BiodataId equals c.Id
                                       join d in db.MSpecializations on a.SpecializationId equals d.Id
                                       //join e in db.TDoctorOffices on b.Id equals e.DoctorId
                                       //join m in db.MMedicalFacilities on e.MedicalFacilityId equals m.Id
                                       where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false /*&& e.IsDelete == false && m.IsDelete == false*/
                                       select new VMListDoctor
                                       {
                                           DoctorId = b.Id,
                                           NameDoctor = c.Fullname,

                                           ImagePath = c.ImagePath,

                                           SpecializationId = d.Id,
                                           NameSpecialist = d.Name,

                                           //MedicalFacilityId = m.Id,

                                           RiwayatPraktek = (from a in db.TDoctorOffices
                                                             join b in db.MMedicalFacilities on a.MedicalFacilityId equals b.Id
                                                             join c in db.MMedicalFacilityCategories on b.MedicalFacilityCategoryId equals c.Id
                                                             join d in db.MLocations on b.LocationId equals d.Id into mloc from tl in mloc.DefaultIfEmpty()
                                                             join e in db.TDoctorOfficeTreatments on a.Id equals e.DoctorOfficeId into te
                                                             from ot in te.DefaultIfEmpty()
                                                             join f in db.TDoctorOfficeTreatmentPrices on ot.Id equals f.Id into tf
                                                             from otp in tf.DefaultIfEmpty()
                                                             where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false
                                                             select new VMRiwayatPraktek
                                                             {
                                                                 DoctorId = a.DoctorId,
                                                                 MedicalFacilityId = b.Id,
                                                                 MedicalFacilityName = b.Name,
                                                                 Specialization = a.Specialization,
                                                                 Location = tl.Name,
                                                                 FullAddress = b.FullAddress,
                                                                 StartDate = a.StartDate,
                                                                 EndDate = a.EndDate,

                                                                 Price = otp.Price ?? 0,
                                                                 PriceStartFrom = otp.PriceStartFrom ?? 0,
                                                                 PriceUntilFrom = otp.PriceUntilFrom ?? 0,

                                                                 LamaBekerja = Convert.ToInt32(DateTime.Now.Year - a.StartDate.Year),

                                                                 CreatedBy = a.CreatedBy,
                                                                 CreatedOn = a.CreatedOn

                                                             }).ToList(),

                                           ListTindakan = (from a in db.TDoctorTreatments
                                                           join b in db.MDoctors on a.DoctorId equals b.Id
                                                           where a.IsDelete == false && b.IsDelete == false
                                                           select new VMTindakanMedis
                                                           {

                                                               DoctorId = b.Id,
                                                               Name = a.Name,

                                                               CreatedBy = a.CreatedBy,
                                                               CreatedOn = a.CreatedOn

                                                           }).ToList()


                                       }).ToList();

            return data;
        }


        [HttpGet("GetProfileDoctor/{IdDoctor}")]
        public VMDoctorSpecialist GetProfileDoctor(int IdDoctor)
        {
            VMDoctorSpecialist data = (from a in db.TCurrentDoctorSpecializations
                                             join b in db.MDoctors on a.DoctorId equals b.Id
                                             join c in db.MBiodata on b.BiodataId equals c.Id
                                             join d in db.MSpecializations on a.SpecializationId equals d.Id
                                             where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false
                                             && a.DoctorId == IdDoctor
                                             select new VMDoctorSpecialist
                                             {
                                                 DoctorId = b.Id,
                                                 BiodataId = c.Id,
                                                 Str = b.Str,

                                                 SpecializationId = d.Id,
                                                 SpecialistName = d.Name,

                                                 Fullname = c.Fullname,
                                                 MobilePhone = c.MobilePhone,
                                                 ImagePath = c.ImagePath,

                                                 //CreatedBy = a.CreatedBy,
                                                 //CreatedOn = a.CreatedOn,

                                                 itemMedical = (from a in db.MMedicalItems
                                                                join b in db.MMedicalItemCategories on a.MedicalItemCategoryId equals b.Id
                                                                where a.IsDelete == false && b.IsDelete == false
                                                                select new VMItemMedical {
                                                                    NameItem = a.Name,

                                                                    MedicalItemCategoryId = b.Id,
                                                                    NameItemCategory = b.Name,

                                                                    PriceMin = a.PriceMin,
                                                                    PriceMax = a.PriceMax
                                                                    
                                                                }).FirstOrDefault(),

                                                 CountAppointment = (from a in db.TAppointments
                                                                     join b in db.MCustomers on a.CustomerId equals b.Id into tc from tcustomer in tc.DefaultIfEmpty()
                                                                     join c in db.TDoctorOffices on a.DoctorOfficeId equals c.Id
                                                                     join d in db.TDoctorOfficeSchedules on a.DoctorOfficeScheduleId equals d.Id
                                                                     join e in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals e.Id
                                                                     where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false && e.IsDelete == false
                                                                     && c.DoctorId == IdDoctor
                                                                     select new VMAppointment
                                                                     {
                                                                         CustomerId = b.Id,
                                                                         DoctorOfficeId = c.Id,
                                                                         DoctorOfficeScheduleId = d.Id,
                                                                         DoctorOfficeTreatmentId = e.Id,

                                                                         AppointmentDate = a.AppointmentDate,

                                                                         CreatedBy = a.CreatedBy,
                                                                         CreatedOn = a.CreatedOn,

                                                                     }).Count(),

                                                 CountChat = (from a in db.TCustomerChatHistories
                                                              join b in db.TCustomerChats on a.CustomerChatId equals b.Id
                                                              join c in db.MDoctors on b.DoctorId equals c.Id into md from mdoc in md.DefaultIfEmpty()
                                                              where a.IsDelete == false && b.IsDelete == false && mdoc.IsDelete == false && mdoc.Id == IdDoctor
                                                              select new VMKonsultasi
                                                              {
                                                                  Id = a.Id,
                                                                  DoctorId = mdoc.Id,

                                                                  CustomerChatId = b.Id,
                                                                  ChatContent = a.ChatContent

                                                              }).Count(),

                                                 ListTindakan = (from a in db.TDoctorTreatments
                                                                 join b in db.MDoctors on a.DoctorId equals b.Id
                                                                 where a.IsDelete == false && b.IsDelete == false
                                                                 && a.DoctorId == IdDoctor
                                                                 select new VMTindakanMedis
                                                                 {

                                                                     DoctorId = b.Id,
                                                                     Name = a.Name,

                                                                     CreatedBy = a.CreatedBy,
                                                                     CreatedOn = a.CreatedOn

                                                                 }).ToList(),

                                                 RiwayatPraktek = (from a in db.TDoctorOffices
                                                                   join b in db.MMedicalFacilities on a.MedicalFacilityId equals b.Id
                                                                   join c in db.MMedicalFacilityCategories on b.MedicalFacilityCategoryId equals c.Id
                                                                   join d in db.MLocations on b.LocationId equals d.Id
                                                                   join e in db.TDoctorOfficeTreatments on a.Id equals e.DoctorOfficeId into te from ot in te.DefaultIfEmpty()
                                                                   join f in db.TDoctorOfficeTreatmentPrices on ot.Id equals f.Id into tf from otp in tf.DefaultIfEmpty()
                                                                   where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false
                                                                   && a.DoctorId == IdDoctor
                                                                   select new VMRiwayatPraktek
                                                                   {
                                                                       DoctorId = a.DoctorId,
                                                                       MedicalFacilityId = b.Id,
                                                                       MedicalFacilityName = b.Name,
                                                                       Specialization = a.Specialization,
                                                                       Location = d.Name,
                                                                       FullAddress = b.FullAddress,
                                                                       StartDate = a.StartDate,
                                                                       EndDate = a.EndDate,

                                                                       Price = otp.Price ?? 0,
                                                                       PriceStartFrom = otp.PriceStartFrom ?? 0,
                                                                       PriceUntilFrom = otp.PriceUntilFrom ?? 0,

                                                                       //LamaBekerja = Convert.ToInt32(Convert.ToDateTime(a.EndDate).Year - a.StartDate.Year),
                                                                       //LamaBekerja = Convert.ToInt32(Convert.ToDateTime(a.EndDate).Year) <= DateTime.Now.Year ?
                                                                       //             Convert.ToInt32(Convert.ToDateTime(a.EndDate).Year - a.StartDate.Year) :
                                                                       //             Convert.ToInt32(DateTime.Now.Year - a.StartDate.Year),

                                                                       LamaBekerja = Convert.ToInt32(DateTime.Now.Year - a.StartDate.Year),

                                                                       CreatedBy = a.CreatedBy,
                                                                       CreatedOn = a.CreatedOn
                                                                   }).ToList(),

                                                 JadwalPraktek = (from a in db.TDoctorOffices
                                                                  join b in db.MMedicalFacilities on a.MedicalFacilityId equals b.Id
                                                                  join c in db.MMedicalFacilitySchedules on b.Id equals c.MedicalFacilityId
                                                                  where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false
                                                                  && a.DoctorId == IdDoctor /*&& a.MedicalFacilityId == b.Id && c.MedicalFacilityId == b.Id*/
                                                                  select new VMJadwalPraktek
                                                                  {
                                                                      JadwalPraktekId = c.MedicalFacilityId ?? 0,
                                                                      Day = c.Day,
                                                                      TimeScheduleStart = c.TimeScheduleStart,
                                                                      TimeScheduleEnd = c.TimeScheduleEnd

                                                                  }).ToList(),

                                                 PendidikanDokter = (from a in db.MDoctors
                                                                     join b in db.MBiodata on a.BiodataId equals b.Id
                                                                     join c in db.MDoctorEducations on a.Id equals c.DoctorId
                                                                     join d in db.MEducationLevels on c.EducationLevelId equals d.Id
                                                                     where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false
                                                                     && c.DoctorId == IdDoctor
                                                                     select new VMPendidikanDokter
                                                                     {

                                                                         DoctorId = a.Id,
                                                                         EducationLevelId = d.Id,
                                                                         NameLevel = d.Name,
                                                                         InstitutionName = c.InstitutionName,
                                                                         Major = c.Major,
                                                                         StartYear = c.StartYear,
                                                                         EndYear = c.EndYear,

                                                                         CreatedBy = c.CreatedBy,
                                                                         CreatedOn = c.CreatedOn

                                                                     }).ToList()


                                             }).FirstOrDefault()!;

            return data;
        }


        [HttpPut("Edit")]
        public VMResponse Edit(MBiodatum data)
        {
            MBiodatum dt = db.MBiodata.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                
                if (data.ImagePath != null)
                {
                    dt.ImagePath = data.ImagePath;
                }
                dt.ModifiedBy = idUserUpdate;
                dt.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = "Data success edited";
                }
                catch (Exception e)
                {
                    respon.Success = false;
                    respon.Message = e.Message;
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "data not found";
            }

            return respon;
        }
    }
}