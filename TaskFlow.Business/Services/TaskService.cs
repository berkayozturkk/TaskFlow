using TaskFlow.Business.DTOs;
using TaskFlow.Business.Interfaces;
using TaskFlow.Data.Repositories.Interfaces;
using TaskFlow.Models.Enums;
using TaskFlow.Service.DTOs;
using EntityTask = TaskFlow.Models.Entities.Task;

namespace TaskFlow.Business.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOperationTypeRepository _operationTypeRepository;

        public TaskService(
            ITaskRepository taskRepository,
            IEmployeeRepository employeeRepository,
            IOperationTypeRepository operationTypeRepository)
        {
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
            _operationTypeRepository = operationTypeRepository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            var taskDtos = new List<TaskDto>();

            foreach (var task in tasks)
            {
                var dto = await MapToDto(task);
                taskDtos.Add(dto);
            }

            return taskDtos;
        }

        public async Task<IEnumerable<TaskDto>> GetFilteredTasksAsync(TaskFilterDto filter)
        {
            var tasks = await _taskRepository.GetFilteredTasksAsync(filter.AnalystId,filter.DeveloperId,filter.Status,filter.Difficulty);
            var taskDtos = new List<TaskDto>();

            foreach (var task in tasks)
            {
                var dto = await MapToDto(task);
                taskDtos.Add(dto);
            }

            return taskDtos;
        }

        private async Task<TaskDto> MapToDto(EntityTask task)
        {
            var dto = new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description ?? "-",
                CreatedDate = task.CreatedDate
            };

            if (task.OperationTypeId > 0)
            {
                var operationType = await _operationTypeRepository.GetByIdAsync(task.OperationTypeId);
                if (operationType != null)
                {
                    dto.OperationTypeName = operationType.Name;
                    dto.DifficultyLevel = (int)operationType.DifficultyLevel;
                }
            }

            dto.Status = task.Status switch
            {
                AssignmentStatus.Pending => "Bekliyor",
                AssignmentStatus.Assigned => "Atandı",
                AssignmentStatus.InProgress => "Devam Ediyor",
                AssignmentStatus.Completed => "Tamamlandı",
                AssignmentStatus.Cancelled => "İptal",
                _ => "Bilinmiyor"
            };

            if (task.AnalystId > 0)
            {
                var analyst = await _employeeRepository.GetByIdAsync(task.AnalystId);
                if (analyst != null)
                {
                    dto.AnalystName = $"{analyst.FirstName} {analyst.LastName}";
                }
            }

            if (task.DeveloperId.HasValue)
            {
                var developer = await _employeeRepository.GetByIdAsync(task.DeveloperId.Value);
                if (developer != null)
                {
                    dto.DeveloperName = $"{developer.FirstName} {developer.LastName}";
                }
            }

            dto.AssignedDate = task.AssignedDate;
            dto.CompletedDate = task.CompletedDate;

            return dto;
        }
    }
}
