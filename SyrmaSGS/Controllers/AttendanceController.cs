using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SyrmaSGS.Models;

namespace SyrmaSGS.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly iServicescs _iServicescs;

        public AttendanceController(iServicescs servicescs)
        {
            _iServicescs = servicescs;
        }
        public IActionResult Attendance()
        {
            BarcodeViewModel objViewModel=new BarcodeViewModel();
            objViewModel = _iServicescs.GetEmployeeDetails(0);
           
            return View(objViewModel);
        }


        //public JsonResult getEmployeeDetail(string empid)
        //{
        //    var empDetails=_iServicescs.GetEmployeeDetail(empid);
        //    return new JsonResult(empDetails);
        //}

        [HttpPost]
        public IActionResult Attendance(BarcodeViewModel model)
        {
            // EmployeeMaster empDetails = new EmployeeMaster();
            // if (ModelState.IsValid)
            //{
            BarcodeViewModel viewModel=new BarcodeViewModel();
            try
            {
                if (model != null)
                {
                    string scannedValue = model.BarcodeValue;

                    // Handle the barcode value here (e.g., database lookup, processing)
                    // ViewBag.Message = "Scanned Barcode Value: " + scannedValue;

                    // Clear the input field after processing
                    // ModelState.Clear();
                    int insertresult = _iServicescs.InsertEmployeeTimesheet(scannedValue);
                    model = _iServicescs.GetEmployeeDetails(insertresult);
                    //if (insertresult == 2)
                    //{
                    //    model.duplicate = true;
                    //    model.error = false;
                    //    model.Try = false;
                    //}
                    //else if (insertresult == 1)
                    //{
                    //    model.duplicate = false;
                    //    model.error = false;
                    //    model.Try = false;
                    //}
                    //else if (insertresult == 3)
                    //{
                    //    model.duplicate = false;
                    //    model.error = true;
                    //    model.Try = false;
                    //}
                    viewModel = model;
                }
                else if (model == null)
                {
                    //model.duplicate = false;
                    //model.error = false;
                    //model.Try = true;
                    ViewBag.error = "true";
                    
                }
                return View(viewModel);

                // model = _iServicescs.GetEmployeeDetail();
                //}

                
            }
            catch (Exception ex)
            {

                return View(viewModel);
            }
            
        }
    }
}
