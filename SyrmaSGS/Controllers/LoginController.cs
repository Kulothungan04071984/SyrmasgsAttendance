using Microsoft.AspNetCore.Mvc;
using SyrmaSGS.Models;

namespace SyrmaSGS.Controllers
{
    public class LoginController : Controller
    {
        private readonly iServicescs _iServicescs;
        public LoginController(iServicescs iservicescs)
        {
                _iServicescs = iservicescs;
        }
        public IActionResult Login()
        {
            UserLogin objLogin = new UserLogin();
            return View(objLogin);
        }
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            var loginResult=_iServicescs.loginValidation(userLogin);
            if (loginResult)
            {
                return RedirectToAction("Attendance", "Attendance");
                userLogin.Isactive=true;
            }
            else
            {
                userLogin.Isactive = false;
                return View(userLogin);
            }
        }
    }
}
