using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace SyrmaSGS.Models
{
    public class DbServices : iServicescs
    {
        private readonly SyrmasgsAttendanceContext _context;
        public DbServices(SyrmasgsAttendanceContext context)
        {
            _context = context;
        }
        public List<EmployeeMaster> GetEmployeeDetails()
        {
            var result=_context.EmployeeMasters.ToList();
            return result;
        }

    }
}
