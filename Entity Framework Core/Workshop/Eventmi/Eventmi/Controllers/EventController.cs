using Microsoft.AspNetCore.Mvc;

namespace Eventmi.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
