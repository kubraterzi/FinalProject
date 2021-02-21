using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class OrdersController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}