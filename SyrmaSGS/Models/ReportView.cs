using Microsoft.AspNetCore.Mvc.Rendering;

namespace SyrmaSGS.Models
{
    public class ReportView
    {
        public DateTime? startDate {  get; set; }

        public DateTime? endDate { get; set; }

        public List<SelectListItem> lstUnitidItems { get; set; }

        public int unitID { get; set; }

        public List<Reports> lstReports { get; set; }

        public List<EmployeeDetails> lstEmployeeDetails { get; set; }

       
    }
}
