//using AspNetCore;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SyrmaSGS.Models;

namespace SyrmaSGS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
        private readonly iServicescs _iServicescs;

        public EmployeeController(iServicescs servicescs)
        {
            _iServicescs = servicescs;
        }
        public ActionResult EmployeeMaster(string departmentId)
        {
            var empDetails = _iServicescs.GetEmployeeDetail();
            return View(empDetails);
        }

        public JsonResult subdepartment(string departmentId)
        {
            var subdeptDetails = _iServicescs.GetSubdepartmentmasters(departmentId);
            return Json(subdeptDetails);
        }



        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeMaster(EmployeeDetails model)
        {
            EmployeeDetails empty = new EmployeeDetails();
            int result = 0;
            if (ModelState.IsValid)
            {
                var employee = new EmployeeMaster
                {
                    //Employeemasterid = model.Employeemasterid,
                    Empcode = model.EMPLOYEEID,
                    Empname = model.EMPLOYEENAME,
                    Dob =Convert.ToDateTime(model.DOB),
                    Doj =Convert.ToDateTime(model.DOJ),
                    Gender = model.GENDER,
                    DesigName = model.DESIGNATIONID.ToString(),
                    DepartmentName = model.DEPARTMENTID.ToString(),
                    Subdepartment = model.SUBDEPARTMENTID.ToString(),
                    Unit = model.UNITID.ToString(),
                    CategoryName = model.CATEGORYID.ToString(),
                    Isactive = true,
                };

                result = _iServicescs.CreateEmployeeDetail(employee);
               
               
            }
            return View(empty);
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
