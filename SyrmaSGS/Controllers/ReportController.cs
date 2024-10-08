using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;
using SyrmaSGS.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;


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

        public IActionResult BulkUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BulkUpload(IFormFile file)
        {
            List<EmployeeMaster> lstEmployeeMater = new List<EmployeeMaster>();
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;

                        EmployeeMaster details;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            details = new EmployeeMaster();
                            details.Empcode = worksheet.Cells[row, 1].GetValue<string>(); // Assuming ID is in the first column
                            details.Empname = worksheet.Cells[row, 2].GetValue<string>();
                            details.Dob = worksheet.Cells[row, 3].GetValue<DateTime>();
                            details.Doj = worksheet.Cells[row, 4].GetValue<DateTime>();
                            details.Gender = worksheet.Cells[row, 5].GetValue<string>();
                            details.DesigName = worksheet.Cells[row, 6].GetValue<string>();
                            details.DepartmentName = worksheet.Cells[row, 7].GetValue<string>();
                            details.Subdepartment = worksheet.Cells[row, 8].GetValue<string>();
                            details.Unit = worksheet.Cells[row, 9].GetValue<string>();
                            details.CategoryName = worksheet.Cells[row, 10].GetValue<string>();
                            details.Isactive = true;// Assuming Name is in the second column

                            if (details.Empcode != null)
                            lstEmployeeMater.Add(details);

                           // var entity =  _iservices.InsertBulkUploads(details);
                            //if (entity != null)
                            //{
                            //    entity.Name = name;
                            //    // Update other properties as necessary
                            //}
                        }
                       // await _context.SaveChangesAsync();
                    }
                }
            }
            return View(lstEmployeeMater); // or whatever view you want to return to
        }

        [HttpPost]
        public IActionResult UploadDetails(List<EmployeeMaster> details)
        {
            var entity = _iservices.InsertBulkUploads(details);
            return RedirectToAction("BulkUpload");
        }

    }
}
