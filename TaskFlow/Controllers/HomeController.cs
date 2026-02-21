using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskFlow.Business.DTOs;
using TaskFlow.Business.Interfaces;
using TaskFlow.Models;
using TaskFlow.Models.Enums;
using TaskFlow.Service.DTOs;

namespace TaskFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;

        public HomeController(ITaskService taskService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(int? analystId, int? developerId, string? status, int? difficulty)
        {
            try
            {
                var filter = new TaskFilterDto
                {
                    AnalystId = analystId,
                    DeveloperId = developerId,
                    Difficulty = difficulty
                };

                if (!string.IsNullOrEmpty(status))
                {
                    filter.Status = status switch
                    {
                        "1" => AssignmentStatus.Pending,
                        "2" => AssignmentStatus.Assigned,
                        "3" => AssignmentStatus.InProgress,
                        "4" => AssignmentStatus.Completed,
                        "5" => AssignmentStatus.Cancelled,
                        _ => (AssignmentStatus?)null
                    };
                }

                // Filtreli taskları getir
                var tasks = await _taskService.GetFilteredTasksAsync(filter);

                // Analist ve Developer listeleri (filtre dropdownları için)
                var (analysts, developers) = await _employeeService.GetAnalystsAndDevelopersAsync();

                var result = new
                {
                    tasks = tasks,
                    analysts = analysts,
                    developers = developers,
                    appliedFilters = new  
                    {
                        analystId = analystId,
                        developerId = developerId,
                        status = status,
                        difficulty = difficulty
                    }
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
