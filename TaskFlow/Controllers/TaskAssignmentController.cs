using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Controllers
{
    public class TaskAssignmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
