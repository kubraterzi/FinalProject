using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AuthController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}