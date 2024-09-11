namespace SyrmaSGS.Models
{
    public class BarcodeViewModel
    {
        public string BarcodeValue { get; set; }
        public EmployeeMaster employeeMaster { get; set; }
        public bool duplicate { get; set; }
        public bool error { get; set; }
        public List<AttendanceTransactionDetail> attendanceTransactions { get; set; }
        public int unit_1 {  get; set; }
        public int unit_2 { get; set; }
        public int unit_3 { get; set; }
        public int shiftCount { get; set; }
        public int overAllCount { get; set; }
        public string gender { get; set; }

        public List<EmployeeDetails> employeeDetails { get; set; }

    }
}
