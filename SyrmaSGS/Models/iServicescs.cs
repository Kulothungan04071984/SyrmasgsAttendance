namespace SyrmaSGS.Models
{
    public interface iServicescs 
    {
        List<AttendanceTransactionDetail> GetEmployeeDetails();

        int InsertEmployeeTimesheet(string empid);
        bool loginValidation(UserLogin userLogin);

        //EmployeeMaster GetEmployeeDetail();

    }
}
