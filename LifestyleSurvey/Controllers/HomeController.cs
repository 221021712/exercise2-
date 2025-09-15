using Microsoft.AspNetCore.Mvc;

namespace LifestyleSurvey.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Survey");
        }

        // You can keep other actions like Privacy if needed
        public IActionResult Privacy()
        {
            return View();
        }
    }
}