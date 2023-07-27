using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.Services;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;

namespace MiniProject319.Controllers
{
        public class DoctorProfileController : Controller
        {
            private readonly DoctorService doctorService;
            private int IdUser = 1;

            public DoctorProfileController(DoctorService _doctorService)
            {
                this.doctorService = _doctorService;
            }
            public async Task<IActionResult> Index()
            {
                int IdDoctor = 2;
                VMMDoctorSpecialist data = await doctorService.GetProfileDoctor(IdDoctor);
                return View(data);
            }

        public IActionResult Create()
        {
            TDoctorTreatment data = new TDoctorTreatment();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TDoctorTreatment dataParam)
        {

            VMResponse respon = await doctorService.Create(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }
            return View(dataParam);
        }

        public async Task<JsonResult> CheckNameIsExist(string Name, int id)
        {
            bool isExist = await doctorService.CheckTDoctorTreatmentByName(Name, id);
            return Json(isExist);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TDoctorTreatment data = await doctorService.GetDataById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {

            VMResponse respon = await doctorService.Delete(id);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }
    }
}
