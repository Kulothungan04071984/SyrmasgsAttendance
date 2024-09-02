namespace SyrmaSGS.Models
{
    public class BarcodeViewModel
    {
        public string BarcodeValue { get; set; }
        public EmployeeMaster employeeMaster { get; set; }
        public bool duplicate { get; set; }

        public bool error { get; set; }
        public List<AttendanceTransactionDetail> attendanceTransactions { get; set; }
    }
}
