using Microsoft.AspNetCore.Mvc;
using TaskFlow.Business.Interfaces;
using TaskFlow.Business.Services;
using TaskFlow.Models;

namespace TaskFlow.Controllers
{
    public class TaskAssignmentController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITaskDistributionService _taskDistributionService;

        public TaskAssignmentController(ITaskService taskService,ITaskDistributionService taskDistributionService)
        {
             _taskService = taskService;
            _taskDistributionService = taskDistributionService;
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

        [HttpPost]
        public async Task<IActionResult> SmartAssign()
        {
            try
            {
                var result = await _taskDistributionService.SmartAssignTasksAsync();

                if (result)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Görevler başarıyla ve adil bir şekilde atandı."
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Görev atama işlemi sırasında bir sorun oluştu."
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Hata: {ex.Message}"
                });
            }
        }
    }
}
