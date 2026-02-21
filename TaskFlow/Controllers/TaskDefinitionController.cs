using Microsoft.AspNetCore.Mvc;
using TaskFlow.Business.DTOs;
using TaskFlow.Business.Interfaces;

namespace TaskFlow.Controllers
{
    public class TaskDefinitionController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IOperationTypeService _operationTypeService;
        private readonly IEmployeeService _employeeService;

        public TaskDefinitionController(ITaskService taskService, IOperationTypeService operationTypeService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _operationTypeService = operationTypeService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var pendingTasks = await _taskService.GetPendingTasksWithoutDifficultyAsync();

            return View(pendingTasks);
        }

        public async Task<IActionResult> Define(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            ViewBag.OperationTypes = await _operationTypeService.GetAllOperationTypesAsync();
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Define(int taskId, int operationTypeId)
        {
            try
            {
                await _taskService.UpdateTaskOperationTypeAsync(taskId, operationTypeId);
                TempData["Success"] = "Task zorluğu başarıyla tanımlandı.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Analysts = await _employeeService.GetAnalystsAsync();
            ViewBag.OperationTypes = await _operationTypeService.GetAllOperationTypesAsync();
            return View(new CreateTaskDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _taskService.CreateTaskAsync(model);
                TempData["Success"] = "Yeni görev başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
    }
}
