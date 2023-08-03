using Microsoft.AspNetCore.Mvc;
using MiniProject319.ViewModels;
using MiniProject319.Services;
using MiniProject319.DataModels;

namespace MiniProject319.Controllers
{
    public class PasienController : Controller
    {
        private PasienService pasienService;
        private HubunganService hubunganService;
        private DarahService darahService;

        private int IdUser = 1;

        public PasienController(PasienService _pasienService, HubunganService _hubunganService, DarahService _darahService)
        {
            this.pasienService = _pasienService;
            this.hubunganService = _hubunganService;
            this.darahService = _darahService;
        }


        public async Task<IActionResult> Index(string sortOrder,
                                                    string searchString,
                                                    string currentFilter,
                                                    int? pageNumber,
                                                    int? pageSize)
        {
            ViewBag.Currentsort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AgeSort = sortOrder == "age" ? "age_desc" : "age";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<VMPasien> data = await pasienService.GetDataByIdParent(2);
            if (!string.IsNullOrEmpty(searchString))
            {

                data = data.Where(a => a.Fullname.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }

            switch (sortOrder)
            {
               
                case "name_desc":
                    data = data.OrderByDescending(a => a.Fullname).ToList();
                    break;
                case "age_desc":
                    data = data.OrderByDescending(a => DateTime.Now.Year - a.Dob.Value.Year).ToList();
                    break;

                case "age":
                    data = data.OrderBy(a => DateTime.Now.Year - a.Dob.Value.Year).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.Fullname).ToList();
                    break;
            }

            return View(PaginatedList<VMPasien>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public async Task<IActionResult> Create()
        {
            VMPasien data = new VMPasien();

            data.ParentBiodataId = 2;//code untuk parent id

            // Get customer relations and blood groups data from the services
            List<VMCustomerRelation> customerRelations = await hubunganService.GetAllData();
            List<MBloodGroup> bloodGroups = await darahService.GetAllData();

            ViewBag.CustomerRelations = customerRelations;
            ViewBag.BloodGroups = bloodGroups; // Set ViewBag.BloodGroups with the bloodGroups data

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMPasien dataParam)
        {
            dataParam.CreatedBy = IdUser;
            dataParam.CreatedOn = DateTime.Now;
            VMResponse respon = await pasienService.Create(dataParam);
            if (respon.Success)
            {
                return Json(new { dataRespon = respon }); //ntar klo udh make json baru dipake
                //return RedirectToAction("Index");
            }
            return PartialView(dataParam);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VMPasien data = new VMPasien();
            data = await pasienService.GetDataById(id);

          

            // Get customer relations and blood groups data from the services
            List<VMCustomerRelation> customerRelations = await hubunganService.GetAllData();
            List<MBloodGroup> bloodGroups = await darahService.GetAllData();

            ViewBag.CustomerRelations = customerRelations;
            ViewBag.BloodGroups = bloodGroups; // Set ViewBag.BloodGroups with the bloodGroups data

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMPasien dataParam)
        {
            dataParam.ModifiedBy = IdUser;
            dataParam.ModifiedOn = DateTime.Now;
            VMResponse respon = await pasienService.Edit(dataParam);
            if (respon.Success)
            {
                //return Json(new { dataRespon = respon });
                return RedirectToAction("Index");
            }
            return PartialView(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            VMPasien data = new VMPasien(); 
            data = await pasienService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int deletedby = IdUser;
            VMResponse respon = await pasienService.Delete(id);

            if (respon.Success)
            {
               return RedirectToAction("Index");
                //return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MultipleDelete(List<int> listId)
        {
            List<string> listName = new List<string>();
            foreach (int item in listId)
            {
                VMPasien data = await pasienService.GetDataById(item);
                listName.Add(data.Fullname);
            }
            //ViewBag.ListId = listId;
            ViewBag.ListName = listName;

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SureMultipleDelete(List<int> listId)
        {

            VMResponse respon = await pasienService.MultipleDelete(listId);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }

    }
}
