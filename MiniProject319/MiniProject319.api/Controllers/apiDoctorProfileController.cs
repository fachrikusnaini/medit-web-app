using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiDoctorProfileController : ControllerBase
    {
        private readonly DB_SpecificationContext db;

        public apiDoctorProfileController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllDataDoctor")]
        public List<VMListDoctor> GetAllDataDoctor()
        {
            List<VMListDoctor> data = (from a in db.TCurrentDoctorSpecializations
                                       join b in db.MDoctors on a.DoctorId equals b.Id
                                       join c in db.MBiodata on b.BiodataId equals c.Id
                                       join d in db.MSpecializations on a.SpecializationId equals d.Id
                                       where a.IsDelete == false && b.IsDelete==false && c.IsDelete == false && d.IsDelete == false
                                       select new VMListDoctor
                                       {
                                           DoctorId = b.Id,
                                           NameDoctor = c.Fullname,

                                           SpecializationId = d.Id,
                                           NameSpecialist = d.Name

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

                                                 CreatedBy = a.CreatedBy,
                                                 CreatedOn = a.CreatedOn,

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
                                                                   where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false
                                                                   && a.DoctorId == IdDoctor
                                                                   select new VMRiwayatPraktek
                                                                   {
                                                                       DoctorId = a.DoctorId,
                                                                       MedicalFacilityId = b.Id,
                                                                       MedicalFacilityName = b.Name,
                                                                       Specialization = a.Specialization,
                                                                       Location = d.Name,
                                                                       StartDate = a.StartDate,
                                                                       EndDate = a.EndDate,

                                                                       CreatedBy = a.CreatedBy,
                                                                       CreatedOn = a.CreatedOn
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
    }
}
