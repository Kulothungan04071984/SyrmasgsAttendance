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
            objViewModel.attendanceTransactions = _iServicescs.GetEmployeeDetails();
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
            try
            {
                string scannedValue = model.BarcodeValue;

                // Handle the barcode value here (e.g., database lookup, processing)
                // ViewBag.Message = "Scanned Barcode Value: " + scannedValue;

                // Clear the input field after processing
                // ModelState.Clear();
                int insertresult = _iServicescs.InsertEmployeeTimesheet(scannedValue);
                if (insertresult == 2)
                {
                    model.duplicate = true;
                    model.error = false;
                }
                else if (insertresult == 1)
                {
                    model.duplicate = false;
                    model.error = false;
                }
                else if (insertresult == 3)
                {
                    model.duplicate = false;
                    model.error = true;
                }
                model.attendanceTransactions = _iServicescs.GetEmployeeDetails();
                //}

                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
    }
}
