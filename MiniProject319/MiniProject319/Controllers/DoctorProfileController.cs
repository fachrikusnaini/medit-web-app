using Microsoft.AspNetCore.Mvc;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class DoctorProfileController : Controller
    {

        private readonly DoctorService doctorService;

        public DoctorProfileController(DoctorService _doctorService)
        {
            this.doctorService = _doctorService;
        }
        public async Task<IActionResult> Detail()
        {
            int IdDoctor = HttpContext.Session.GetInt32("BiodataId") ?? 0;
            //int IdDoctor = 1;
            VMDoctorSpecialist data = await doctorService.GetProfileDoctor(IdDoctor);
            return View(data);
        }
    }
}
