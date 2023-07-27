using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;
using System.Drawing;

namespace MiniProject319.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiDoctorProfileController : ControllerBase
    {
        private readonly DB_SpecificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;
        private int DoctorId = 2;

        public apiDoctorProfileController(DB_SpecificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetProfileDoctor/{IdDoctor}")]
        public VMMDoctorSpecialist GetProfileDoctor(int IdDoctor)
        {
            VMMDoctorSpecialist data = (from a in db.TCurrentDoctorSpecializations
                                       join b in db.MDoctors on a.DoctorId equals b.Id
                                       join c in db.MBiodata on b.BiodataId equals c.Id
                                       join d in db.MSpecializations on a.SpecializationId equals d.Id
                                       where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false
                                       && a.DoctorId == IdDoctor
                                       select new VMMDoctorSpecialist
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
                                                               join b in db.MCustomers on a.CustomerId equals b.Id into tc
                                                               from tcustomer in tc.DefaultIfEmpty()
                                                               join c in db.TDoctorOffices on a.DoctorOfficeId equals c.Id
                                                               join d in db.TDoctorOfficeSchedules on a.DoctorOfficeScheduleId equals d.Id
                                                               join e in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals e.Id
                                                               where a.IsDelete == false && b.IsDelete == false && c.IsDelete == false && d.IsDelete == false && e.IsDelete == false
                                                               && c.DoctorId == IdDoctor
                                                               select new VMMApointment
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
                                                           select new VMMTindakanMedis
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
                                                             select new VMMRiwayatPraktek
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
                                                               select new VMMPendidikanDoctor
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

        [HttpPost("Save")]
        public VMResponse Save(TDoctorTreatment data)
        {
           
            data.DoctorId = DoctorId;
            data.CreatedBy = IdUser;
            data.CreatedOn = DateTime.Now;
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

        [HttpDelete("Delete/{id}")]
        public VMResponse Delete(int id)
        {
            TDoctorTreatment dt = db.TDoctorTreatments.Where(a => a.Id == id).FirstOrDefault()!;
            if (dt != null)
            {
                dt.IsDelete = true;
                dt.CreatedBy = IdUser;
                dt.CreatedOn = DateTime.Now;

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