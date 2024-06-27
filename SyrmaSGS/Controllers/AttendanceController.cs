using Microsoft.AspNetCore.Mvc;
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
            var result=_iServicescs.GetEmployeeDetails();
            return View(result);
        }
    }
}
