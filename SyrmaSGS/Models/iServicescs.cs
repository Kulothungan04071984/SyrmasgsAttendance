using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace SyrmaSGS.Models
{
    public interface iServicescs 
    {
        BarcodeViewModel GetEmployeeDetails(int insertresult);

        int InsertEmployeeTimesheet(string empid);
        bool loginValidation(UserLogin userLogin);

        EmployeeDetails GetEmployeeDetail();

        List<SelectListItem> GetUnitDetails();

        List<EmployeeDetails> GetAttendancedetails(ReportView rview);

        List<SubDepartmentMaster> GetSubdepartmentmasters(string departmentId);


        int CreateEmployeeDetail(EmployeeMaster employee);

        int InsertBulkUploads(List<EmployeeMaster> employeeDetails);

    }
}
