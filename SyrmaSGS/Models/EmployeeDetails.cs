using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<SelectListItem> ddlDepartment { get; set; }

        public List<SelectListItem> ddlDesignation { get; set; }

        public List<SelectListItem> ddlSubDepartment { get; set; }

        public List<SelectListItem> ddlCategory { get; set; }

        public List<SelectListItem> ddlUnit { get; set; }

        public int DEPARTMENTID { get; set; }

        public int  DESIGNATIONID { get; set; }

        public int SUBDEPARTMENTID { get; set; }

        public int CATEGORYID { get; set; }

        public int UNITID { get; set; }

        public DateTime? CURRENTDATE { get; set; }    

    }
}
