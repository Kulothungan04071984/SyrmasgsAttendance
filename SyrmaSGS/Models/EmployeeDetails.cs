using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Xml.Linq;

namespace SyrmaSGS.Models
{
    public class EmployeeDetails
    {
        public string EMPLOYEEID { get; set; }

        public string EMPLOYEENAME { get; set; }

        public string DOB { get; set; }

        public string DOJ {  get; set; }

        public string GENDER { get; set; }

        public string CATEGORY_NAME { get; set; }

        public string DESIGNATION_NAME { get; set; }

        public string DEPARTMENT_NAME {  get; set; }

        public string E_MAILID { get; set; }

        public string UNIT { get; set; }

        public string REPORTING_MANAGER_ID { get; set; }

        public string REPORTING_MANAGER_NAME { get; set; }

        public string REPORTING_MANAGER_E_MAIL_ID { get; set; }

        public DateTime? STARTTIME { get; set; }

        public DateTime? ENDTIME { get; set; }

    }
}
