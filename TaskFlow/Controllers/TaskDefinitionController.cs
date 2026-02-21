using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Controllers
{
    public class TaskDefinitionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
