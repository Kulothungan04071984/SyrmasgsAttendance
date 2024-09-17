namespace SyrmaSGS.Models
{
    public interface iServicescs 
    {
        BarcodeViewModel GetEmployeeDetails();

        int InsertEmployeeTimesheet(string empid);
        bool loginValidation(UserLogin userLogin);

        EmployeeDetails GetEmployeeDetail();

        SubDepartmentMaster GetSubdepartmentmasters(string departmentId);


        int CreateEmployeeDetail(EmployeeMaster employee);

    }
}
