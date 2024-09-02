using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SyrmaSGS.Models;
using System.Diagnostics;

namespace SyrmaSGS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly iServicescs _iServicescs;
        public HomeController(ILogger<HomeController> logger, iServicescs iServicescs)
        {
            _logger = logger;
            _iServicescs = iServicescs;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BarcodeViewModel model)
        {
           // if (ModelState.IsValid)
            //{
                string scannedValue = model.BarcodeValue;
                
                // Handle the barcode value here (e.g., database lookup, processing)
                ViewBag.Message = "Scanned Barcode Value: " + scannedValue;
              //  var empDetails = _iServicescs.GetEmployeeDetail(scannedValue);
                //model.employeeMaster = empDetails;
                // Clear the input field after processing

             //   ModelState.Clear();
           // }

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
