using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;
using SyrmaSGS.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

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
            //string datastr=Convert.ToString(rview.startDate?.ToString("yyyy-MM-dd"));
            //CultureInfo provider = CultureInfo.InvariantCulture;
            //DateTime parsedDate = DateTime.ParseExact(datastr, "yyyy-MM-dd", provider);
            //rview.startDate = parsedDate;
            //string dataendstr = Convert.ToString(rview.endDate?.ToString("yyyy-MM-dd"));
            //DateTime parsedDateend = DateTime.ParseExact(dataendstr, "yyyy-MM-dd", provider);
            //rview.endDate= parsedDateend;
            //rview.endDate = DateTime.Parse(dataendstr);
            objReport.lstEmployeeDetails=_iservices.GetAttendancedetails(rview);
           return View (objReport);

        }
    }
}
