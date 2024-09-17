using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Globalization;
namespace SyrmaSGS.Models
{
    public class DbServices : iServicescs
    {
        private readonly SyrmasgsAttendanceContext _context;
        public DbServices(SyrmasgsAttendanceContext context)
        {
            _context = context;
        }
        public BarcodeViewModel GetEmployeeDetails(int insertresult)
        {
            var objModel = new BarcodeViewModel();
            try
            {

                //  var result = new List<AttendanceTransactionDetail>();
                // var result = _context.AttendanceTransactionDetails.join(_context.DepartMentMasters, deptid=> deptid.)
                //   Where(a => a.CurrentDate == DateTime.Today && a.IsActive == true).OrderByDescending(a => a.TransId).ToList();
                //var result =_context.AttendanceTransactionDetails.Join(
                //_context.DepartMentMasters,a=>a.DepartMemtId,d=>d.DeptId,(a,d)=> new (a.DepartMemtId,d.DepartMentName) )
                var result = (from attendance in _context.AttendanceTransactionDetails
                              join dept in _context.DepartMentMasters on attendance.DepartMemtId equals dept.DeptId
                              join desi in _context.DesignationMasters on attendance.DesignationId equals desi.DesiId
                              join unit in _context.UnitMasters on attendance.UnitId equals unit.UnitId
                              where attendance.CurrentDate == DateTime.Today &&
                              attendance.IsActive == true
                              select new EmployeeDetails
                              {
                                  EMPLOYEEID = attendance.EmpId,
                                  EMPLOYEENAME = attendance.EmpName,
                                  DEPARTMENT_NAME = dept.DepartMentName,
                                  DESIGNATION_NAME = desi.DesignationName,
                                  UNIT = unit.UnitName,
                                  STARTTIME = attendance.StartTime,
                                  ENDTIME = attendance.EndTime,

                              }).ToList();

                objModel.employeeDetails = result;
                objModel.overAllCount = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.IsActive == true).Count();
                objModel.unit_1 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 2 && a.IsActive == true).Count();
                objModel.unit_2 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 3 && a.IsActive == true).Count();
                objModel.unit_3 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 4 && a.IsActive == true).Count();
                if (insertresult == 2)
                {
                    objModel.duplicate = true;
                    objModel.error = false;
                    objModel.Try = false;
                }
                else if (insertresult == 1)
                {
                    objModel.duplicate = false;
                    objModel.error = false;
                    objModel.Try = false;
                }
                else if (insertresult == 3)
                {
                    objModel.duplicate = false;
                    objModel.error = true;
                    objModel.Try = false;
                }

                return objModel;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "InsertEmployeeTimesheet");
                return new BarcodeViewModel();
            }

        }

        public bool loginValidation(UserLogin userLogin)
        {
            var result = false;
            try
            {
                result = _context.UserLogins.Where(a => a.Userid == userLogin.Userid && a.Userpassword == userLogin.Userpassword).Any();
                return result;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "loginValidation");
                return result;
            }
        }
        public int InsertEmployeeTimesheet(string empid)
        {
            AttendanceTransactionDetail timeDetails = new AttendanceTransactionDetail();
            int insertResult = 0;
            try
            {
                var empDetails = _context.EmployeeMasters.Where(a => a.Empcode.ToString() == empid).FirstOrDefault();
                var exsitTime = _context.AttendanceTransactionDetails
                    .Where(a => a.CurrentDate == DateTime.Today
                    && a.EmpId == empDetails.Empcode.ToString())
                    .Select(a => new AttendanceTransactionDetail()
                    { TransId = a.TransId, EmpId = a.EmpId, StartTime = a.StartTime, EndTime = a.EndTime })
                    .OrderByDescending(a => a.TransId).FirstOrDefault();

                if (empDetails != null && exsitTime != null)
                {


                    if ((exsitTime.StartTime != null && exsitTime.EndTime != null) || (exsitTime.StartTime != null && exsitTime.EndTime == null))
                    {
                        DateTime now = DateTime.Now;
                        DateTime dateTimeWithoutSeconds = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                        TimeSpan? S_duration = dateTimeWithoutSeconds - exsitTime.StartTime;
                        TimeSpan? E_duration = dateTimeWithoutSeconds - exsitTime.EndTime;
                        if (S_duration.HasValue)
                        {
                            if (S_duration.Value.TotalMinutes > 5)
                            {


                                if (exsitTime.StartTime != null && exsitTime.EndTime == null)
                                {

                                    _context.AttendanceTransactionDetails.Where(a => a.TransId == exsitTime.TransId).ToList().ForEach(a => a.EndTime = dateTimeWithoutSeconds);
                                    _context.SaveChanges();
                                    insertResult = 1;
                                }
                                else if ((exsitTime.StartTime != null && exsitTime.EndTime != null) && E_duration.Value.TotalMinutes > 5)
                                {
                                    _context.AttendanceTransactionDetails.Where(a => a.EmpId == empid && a.CurrentDate == DateTime.Today).ToList().ForEach(a => a.IsActive = false);
                                    _context.SaveChanges();
                                    var results = insertvalues(timeDetails, empDetails);
                                    insertResult = results;
                                }
                                else if (E_duration.Value.TotalMinutes <= 5)
                                {
                                    insertResult = 2;
                                }

                            }
                            else if (S_duration.Value.TotalMinutes <= 5)
                            {
                                insertResult = 2;
                            }
                            else if (E_duration.HasValue)
                            {
                                if (E_duration.Value.TotalMinutes > 5)
                                {
                                    _context.AttendanceTransactionDetails.Where(a => a.TransId == exsitTime.TransId).ToList().ForEach(a => a.IsActive = false);
                                    _context.SaveChanges();
                                    var dur = insertvalues(timeDetails, empDetails);
                                    insertResult = dur;
                                }
                                else if (E_duration.Value.TotalMinutes <= 5)
                                {
                                    insertResult = 2;
                                }

                            }

                        }

                        //}
                    }
                }
                else if (exsitTime == null)
                {
                    var result = insertvalues(timeDetails, empDetails);
                    insertResult = result;

                }
                else if (exsitTime.StartTime == null && exsitTime.EndTime == null)
                {
                    var res = insertvalues(timeDetails, empDetails);
                    insertResult = res;
                }

                return insertResult;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "InsertEmployeeTimesheet");
                return insertResult = 3;
            }
        }

        public int insertvalues(AttendanceTransactionDetail timeDetails, EmployeeMaster empDetails)
        {
            int insertResults = 0;
            try
            {
                timeDetails.EmpId = empDetails.Empcode.ToString();
                timeDetails.EmpName = empDetails.Empname;
                timeDetails.DesignationId = Convert.ToInt32(empDetails.DesigName);
                timeDetails.DepartMemtId = Convert.ToInt32(empDetails.DepartmentName);
                timeDetails.UnitId = Convert.ToInt32(empDetails.Unit);
                timeDetails.CurrentDate = DateTime.Today;
                timeDetails.IsActive = true;
                DateTime now = DateTime.Now;
                DateTime dateTimeWithoutSeconds = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                timeDetails.StartTime = dateTimeWithoutSeconds;
                _context.AttendanceTransactionDetails.Add(timeDetails);
                _context.SaveChanges();
                return insertResults = 1;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "insertvalues");
                return insertResults;
            }
        }

        public List<EmployeeDetails> GetAttendancedetails(ReportView rview)
        {
            var result = (from attendance in _context.AttendanceTransactionDetails
                          join dept in _context.DepartMentMasters on attendance.DepartMemtId equals dept.DeptId
                          join desi in _context.DesignationMasters on attendance.DesignationId equals desi.DesiId
                          join unit in _context.UnitMasters on attendance.UnitId equals unit.UnitId
                          where attendance.StartTime >= rview.startDate && attendance.EndTime >= rview.endDate &&
                          attendance.UnitId == (attendance.UnitId == 0 ? attendance.UnitId : rview.unitID)
                          select new EmployeeDetails
                          {
                              EMPLOYEEID = attendance.EmpId,
                              EMPLOYEENAME = attendance.EmpName,
                              DEPARTMENT_NAME = dept.DepartMentName,
                              DESIGNATION_NAME = desi.DesignationName,
                              UNIT = unit.UnitName,
                              STARTTIME = attendance.StartTime,
                              ENDTIME = attendance.EndTime,
                              CURRENTDATE= attendance.CurrentDate,
                          }).ToList();

            return result;
        }

        public List<SelectListItem> GetUnitDetails()
        {
            var unitlist= _context.UnitMasters.Where(a => a.IsActive == true).OrderBy(a => a.UnitName).Select(e => new SelectListItem { Value = e.UnitId.ToString(), Text = e.UnitName }).ToList();
            unitlist.Insert(0, (new SelectListItem { Value = "0", Text = "All" }));
            return unitlist;
        }



        public EmployeeDetails GetEmployeeDetail()
        {
            EmployeeDetails objEmployeeDetails = new EmployeeDetails();
            try
            {
                //// objEmployeeDetails.employeeMaster = _context.EmployeeMasters.Where(a => a.EmpCode == Convert.ToString(empid) ).FirstOrDefault();
                // objEmployeeDetails.overAllCount = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.IsActive == true).Count();
                // objEmployeeDetails.unit_1 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 2 && a.IsActive == true).GroupBy(a => a.UnitId).Count();
                // objEmployeeDetails.unit_2 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 3 && a.IsActive == true).GroupBy(a => a.UnitId).Count();
                // objEmployeeDetails.unit_3 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 4 && a.IsActive == true).GroupBy(a => a.UnitId).Count();
                objEmployeeDetails.ddlDepartment = _context.DepartMentMasters.Where(a => a.IsActive == true).OrderBy(a => a.DepartMentName).Select(e => new SelectListItem { Value = e.DeptId.ToString(), Text = e.DepartMentName }).ToList();
                objEmployeeDetails.ddlDepartment.Insert(0, (new SelectListItem { Value = "0", Text = "Select" }));
                //objEmployeeDetails.ddlSubDepartment = _context.SubDepartmentMasters.Where(a => a.DDpartmentname == departmentId && a.IsActive==true).OrderBy(a => a.SubDepartmentName).Select(e => new SelectListItem{Value = e.SubDepartmentid.ToString(),Text = e.SubDepartmentName}).ToList();
                //objEmployeeDetails.ddlSubDepartment.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                ////objEmployeeDetails.ddlSubDepartment = _context.SubDepartmentMasters.Where(a => a.IsActive == true).OrderBy(a => a.SubDepartmentName).Select(e => new SelectListItem { Value = e.SubDepartmentid.ToString(), Text = e.SubDepartmentName }).ToList();
                //objEmployeeDetails.ddlSubDepartment.Insert(0, (new SelectListItem { Value = "0", Text = "Select" }));
                objEmployeeDetails.ddlDesignation = _context.DesignationMasters.Where(a => a.IsActive == true).OrderBy(a => a.DesignationName).Select(e => new SelectListItem { Value = e.DesiId.ToString(), Text = e.DesignationName }).ToList();
                objEmployeeDetails.ddlDesignation.Insert(0, (new SelectListItem { Value = "0", Text = "Select" }));
                objEmployeeDetails.ddlCategory = _context.EmployeeMasters.Where(a => a.Isactive == true).GroupBy(a => a.CategoryName).Select(g => new SelectListItem { Value = g.Key, Text = g.Key }).OrderBy(item => item.Text).ToList();
                objEmployeeDetails.ddlCategory.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                objEmployeeDetails.ddlUnit = _context.UnitMasters.Where(a => a.IsActive == true).OrderBy(a => a.UnitName).Select(e => new SelectListItem { Value = e.UnitId.ToString(), Text = e.UnitName }).ToList();
                objEmployeeDetails.ddlSubDepartment.Insert(0, (new SelectListItem { Value = "0", Text = "Select" }));

                return objEmployeeDetails;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "GetEmployeeDetail");
                return objEmployeeDetails;
            }
        }

        public SubDepartmentMaster GetSubdepartmentmasters(string departmentId)
        {
            SubDepartmentMaster objSubdepartmentmasters = new SubDepartmentMaster();
            try
            {

                objSubdepartmentmasters.ddlSubDepartment = _context.SubDepartmentMasters.Where(a => a.DDpartmentname == departmentId && a.IsActive == true).OrderBy(a => a.SubDepartmentName).Select(e => new SelectListItem { Value = e.SubDepartmentid.ToString(), Text = e.SubDepartmentName }).ToList();
                objSubdepartmentmasters.ddlSubDepartment.Insert(0, new SelectListItem { Value = "0", Text = "Select" });


                return objSubdepartmentmasters;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "GetSubdepartmentmasters");
                return objSubdepartmentmasters;
            }
        }

        public int CreateEmployeeDetail(EmployeeMaster employee)
        {
            EmployeeMaster objEmployeeMaster = new EmployeeMaster();
            int insertResult = 0;
            try
            {
                //// objEmployeeDetails.employeeMaster = _context.EmployeeMasters.Where(a => a.EmpCode == Convert.ToString(empid) ).FirstOrDefault();
                // objEmployeeDetails.overAllCount = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.IsActive == true).Count();
                // objEmployeeDetails.unit_1 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 2 && a.IsActive == true).GroupBy(a => a.UnitId).Count();
                // objEmployeeDetails.unit_2 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 3 && a.IsActive == true).GroupBy(a => a.UnitId).Count();
                // objEmployeeDetails.unit_3 = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.UnitId == 4 && a.IsActive == true).GroupBy(a => a.UnitId).Count();
                //objEmployeeMaster.Empcode = employee.Empcode;
                //objEmployeeMaster.ddlDepartment.Insert(0, (new SelectListItem { Value = "0", Text = "Select" }));
                //objEmployeeMaster.ddlSubDepartment = _context.SubDepartmentMasters.Where(a => a.IsActive == true).OrderBy(a => a.SubDepartmentName).Select(e => new SelectListItem { Value = e.SubDepartmentid.ToString(), Text = e.SubDepartmentName }).ToList();
                //objEmployeeMaster.ddlSubDepartment.Insert(0, (new SelectListItem { Value = "0", Text = "Select" }));
                //objEmployeeMaster.ddlDesignation = _context.DesignationMasters.Where(a => a.IsActive == true).OrderBy(a => a.DesignationName).Select(e => new SelectListItem { Value = e.DesiId.ToString(), Text = e.DesignationName }).ToList();
                //objEmployeeMaster.ddlDesignation.Insert(0, (new SelectListItem { Value = "0", Text = "Select" }));
                //objEmployeeMaster.ddlCategory = _context.EmployeeMasters.Where(a => a.Isactive == true).GroupBy(a => a.CategoryName).Select(g => new SelectListItem { Value = g.Key, Text = g.Key }).OrderBy(item => item.Text).ToList();
                //objEmployeeMaster.ddlCategory.Insert(0, new SelectListItem { Value = "0", Text = "Select" });
                //objEmployeeMaster.ddlUnit = _context.UnitMasters.Where(a => a.IsActive == true).OrderBy(a => a.UnitName).Select(e => new SelectListItem { Value = e.UnitId.ToString(), Text = e.UnitName }).ToList();
                //objEmployeeMaster.ddlSubDepartment.Insert(0, (new SelectListItem { Value = "0", Text = "Select" }));
                _context.EmployeeMasters.Add(employee);
                _context.SaveChanges();
                insertResult = 1;
                return insertResult;
            }
            catch (Exception ex)
            {
                insertResult = 0;
                writeErrorMessage(ex.Message.ToString(), "GetEmployeeDetail");
                return insertResult;
            }
        }

        public void writeErrorMessage(string errorMessage, string functionName)
        {
            var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SyrmaSGSAttendance" + "\\" + DateTime.Now.ToString("dd-MM-yyyy");

            if (!Directory.Exists(systemPath))
            {
                Directory.CreateDirectory(systemPath);
            }

            string WrErrorLog = String.Format(@"{0}\{1}.txt", systemPath, "ErrorLogSyrmaSGSAttendance");
            using (StreamWriter errLogs = new StreamWriter(WrErrorLog, true))
            {
                errLogs.WriteLine("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                errLogs.WriteLine("---------------------------------------------------" + DateTime.Now + "----------------------------------------------" + Environment.NewLine);
                errLogs.WriteLine(errorMessage + Environment.NewLine + "-----" + functionName);
                errLogs.Close();
            }

        }

    }
}
 