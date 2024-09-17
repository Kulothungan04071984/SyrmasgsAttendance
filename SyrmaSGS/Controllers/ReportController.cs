using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SyrmaSGS.Models;

namespace SyrmaSGS.Controllers
{
    public class ReportController : Controller
    {
        private readonly iServicescs _iservices;
        public ReportController(iServicescs iServicescs)
        {
              _iservices = iServicescs;
        }
        public IActionResult AttendanceReport()
        {
            ReportView objReport=new ReportView();
           // List<EmployeeDetails> lstreports = new List<EmployeeDetails>();
            //var lstunitid = new List<UnitMaster>();
            //lstunitid.Add(new UnitMaster { UnitId = 1, UnitName = "unit1" });
            //lstunitid.Add(new UnitMaster { UnitId = 2, UnitName = "unit2" });
            //lstunitid.Add(new UnitMaster { UnitId = 3, UnitName = "unit3" });
            //var lstUnit = new List<SelectListItem>();
            //foreach (var item in lstunitid)
            //{
            //    lstUnit.Add(new SelectListItem { Value = item.UnitId.ToString(), Text = item.UnitName });
            //}
            objReport.lstUnitidItems = _iservices.GetUnitDetails();

            //objReport.lstEmployeeDetails = lstreports;

            
            return View(objReport);
        }

        [HttpPost]
        public IActionResult AttendanceReport(ReportView rview)
        {
            ReportView objReport = new ReportView();
            objReport.lstUnitidItems = _iservices.GetUnitDetails(); 
           objReport.lstEmployeeDetails=_iservices.GetAttendancedetails(rview);
           return View (objReport);

        }
    }
}
