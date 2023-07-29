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

        public async Task<IActionResult> ListDokter(string searchString,
                                                    string searchName,
                                                    string searchLocation,
                                                    string searchTindakan,
                                                    string currentFilter,
                                                    int? pageNumber,
                                                    int? pageSize)
        {
            ViewBag.CurrentPageSize = pageSize;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<VMListDoctor> data = await doctorService.GetAllDataDoctor();

            //if (!string.IsNullOrEmpty(searchString))
            //{
                if (!string.IsNullOrEmpty(searchName))
                {
                    data = data.Where(a => a.NameSpecialist.ToLower().Contains(searchString.ToLower())
                    && a.NameDoctor.ToLower().Contains(searchName.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(searchLocation))
                {
                    data = data.Where(a => a.NameSpecialist.ToLower().Contains(searchString.ToLower())
                    && a.LocationName.ToLower().Contains(searchLocation.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(searchTindakan))
                {
                    data = data.Where(a => a.NameSpecialist.ToLower().Contains(searchString.ToLower())
                    && a.TindakanName.ToLower().Contains(searchTindakan.ToLower())).ToList();
                }


            //}
            return View(PageInatedList<VMListDoctor>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 2));
        }

        public async Task<IActionResult> ModalCariDokter()
        {
            VMCariDokter data = await doctorService.GetCariDoctor();
            return PartialView(data);
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
