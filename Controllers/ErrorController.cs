using Microsoft.AspNetCore.Mvc;

namespace SSVH_Consultation_Poratl.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
