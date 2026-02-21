using Microsoft.AspNetCore.Mvc;
using TaskFlow.Business.Interfaces;
using TaskFlow.Models;

namespace TaskFlow.Controllers
{
    public class TaskAssignmentController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskAssignmentController(ITaskService taskService)
        {
             _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new TaskAssignmentViewModel
            {
                PendingTasks = await _taskService.GetUnassignedTasksAsync(),
                AssignedTasks = await _taskService.GetAssignedTasksAsync()  
            };

            return View(viewModel);
        }
    }
}
