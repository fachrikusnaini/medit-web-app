using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MiniProject319.Services;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
    public class DokterController : Controller
    {
        private readonly DoctorService doctorService;

        public DokterController(DoctorService _doctorService)
        {
            this.doctorService = _doctorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListDokter()
        {
            List<VMListDoctor> data = await doctorService.GetAllDataDoctor();
            return View(data);
        }

        public IActionResult ModalCariDokter()
        {
            return PartialView();
        }

        public async Task<IActionResult> DetailDokter()
        {
            //int IdDoctor = HttpContext.Session.GetInt32("BiodataId") ?? 0;
            int id = 1;
            VMDoctorSpecialist data = await doctorService.GetProfileDoctor(id);
            return View(data);
        }
    }
}
