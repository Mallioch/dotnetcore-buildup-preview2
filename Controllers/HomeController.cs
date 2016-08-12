using Microsoft.AspNetCore.Mvc;

namespace BuildUp.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            ViewData["Greeting"] = "Hello from the server!";
            return View();
        }
    }
}