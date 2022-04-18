using Microsoft.AspNetCore.Mvc;

namespace GoKartCR.Controllers
{
    public class AdministradorController : Controller
    {
        public IActionResult preguntasFrecuentes()
        {
            return View();
        }
    }
}
