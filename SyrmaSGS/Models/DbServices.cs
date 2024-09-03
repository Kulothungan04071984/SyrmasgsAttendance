﻿using Microsoft.AspNetCore.Http;
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
        public List<AttendanceTransactionDetail> GetEmployeeDetails()
        {
            try
            {

              //  var result = new List<AttendanceTransactionDetail>();
                var result = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.IsActive == true).OrderByDescending(a => a.TransId).ToList();
                return result;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "InsertEmployeeTimesheet");
                return new List<AttendanceTransactionDetail>();
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
            catch(Exception ex)
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
                var empDetails = _context.EmployeeMasters.Where(a => a.EmployeeId.ToString() == empid && a.IsActive == "1").FirstOrDefault();
                var exsitTime = _context.AttendanceTransactionDetails
                    .Where(a => a.CurrentDate == DateTime.Today && a.EmpId == empDetails.EmployeeId.ToString())
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
                timeDetails.EmpId = empDetails.EmployeeId.ToString();
                timeDetails.EmpName = empDetails.FirstName;
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

        public BarcodeViewModel GetEmployeeDetail(string empid)
        {
            BarcodeViewModel objEmployeeDetails = new BarcodeViewModel();
            try
            {
                objEmployeeDetails.employeeMaster = _context.EmployeeMasters.Where(a => a.EmployeeId == Convert.ToDouble(empid)).FirstOrDefault();
                objEmployeeDetails.overAllCount = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.IsActive == true).Count();
                // objEmployeeDetails.overAllCount = _context.AttendanceTransactionDetails.Where(a => a.CurrentDate == DateTime.Today && a.IsActive == true && a.loc).Count();
                return objEmployeeDetails;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "GetEmployeeDetail");
                return objEmployeeDetails;
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
 