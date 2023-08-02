using Microsoft.AspNetCore.Mvc;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class DoctorProfileController : Controller
    {

        private readonly DoctorService doctorService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DoctorProfileController(DoctorService _doctorService)
        {
            this.doctorService = _doctorService;
        }
        public async Task<IActionResult> Detail()
        {
            int IdDoctor = HttpContext.Session.GetInt32("DoctorId") ?? 0;
            //int IdDoctor = 1;
            VMDoctorSpecialist data = await doctorService.GetProfileDoctor(IdDoctor);
            return View(data);
        }

        public async Task<IActionResult> EditFoto()
        {
            int IdDoctor = HttpContext.Session.GetInt32("DoctorId") ?? 0;
            //int IdDoctor = 1;
            VMDoctorSpecialist data = await doctorService.GetProfileDoctor(IdDoctor);
            return PartialView(data);
        }

        public string Upload(VMDoctorSpecialist dataParam)
        {
            string uniqueFileName = "";

            if (dataParam.ImageFile != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + dataParam.ImageFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dataParam.ImageFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpPost]
        public async Task<IActionResult> EditFoto(VMDoctorSpecialist dataParam)
        {
            if (dataParam.ImageFile != null)
            {
                dataParam.ImagePath = Upload(dataParam);
            }

            VMResponse respon = await doctorService.Edit(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }
    }
}
