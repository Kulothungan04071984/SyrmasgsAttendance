namespace SyrmaSGS.Models
{
    public interface iServicescs 
    {
        BarcodeViewModel GetEmployeeDetails();

        int InsertEmployeeTimesheet(string empid);
        bool loginValidation(UserLogin userLogin);

        EmployeeDetails GetEmployeeDetail();

    }
}
