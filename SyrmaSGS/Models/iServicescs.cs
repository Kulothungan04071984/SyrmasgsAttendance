namespace SyrmaSGS.Models
{
    public interface iServicescs 
    {
        List<AttendanceTransactionDetail> GetEmployeeDetails();

        int InsertEmployeeTimesheet(string empid);

        //EmployeeMaster GetEmployeeDetail();

    }
}
