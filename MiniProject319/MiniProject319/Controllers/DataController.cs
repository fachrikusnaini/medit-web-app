using Microsoft.AspNetCore.Mvc;
using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using MiniProject319.Services;

namespace MiniProject319.Controllers
{
    public class DataController : Controller
    {
        private DataService dataService;
        private int IdUser = 1;

        public DataController(DataService _dataService)
        {
            this.dataService = _dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Edit(int id)
        //{
        //    VMUser data = new VMUser();
        //    data = profileService.GetDataById(id);
        //    MUser 
        //}

    }
}
