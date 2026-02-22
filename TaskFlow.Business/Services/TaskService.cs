using Microsoft.EntityFrameworkCore;
using TaskFlow.Business.DTOs;
using TaskFlow.Business.Interfaces;
using TaskFlow.Data.Repositories;
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

        public async Task<TaskDto> GetByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            var dto = await MapToDto(task);
            return dto;
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

        public async Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(AssignmentStatus status)
        {
            var tasks = await _taskRepository.GetTasksByStatusAsync(status);

            var taskDtos = new List<TaskDto>();
            foreach (var task in tasks)
            {
                taskDtos.Add(await MapToDto(task));
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

        public async Task UpdateTaskOperationTypeAsync(int taskId, int operationTypeId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new Exception("Task bulunamadı!");

            var operationType = await _operationTypeRepository.GetByIdAsync(operationTypeId);
            if (operationType == null)
                throw new Exception("Geçersiz işlem tipi!");

            task.OperationTypeId = operationTypeId;
            task.OperationType = operationType;
            task.AnalystId = 1;

            await _taskRepository.Update(task);
        }

        public async Task<IEnumerable<TaskDto>> GetPendingTasksWithoutDifficultyAsync()
        {
            var allTasks = await _taskRepository.GetAllAsync();

            //var tasks = await _taskRepository.GetPendingTasksWithoutDifficultyAsync();

            var taskDtos = new List<TaskDto>();
            foreach (var task in allTasks.Where(t => t.Status == AssignmentStatus.Pending && t.OperationTypeId == 0))
                taskDtos.Add(await MapToDto(task));

            return taskDtos;
        }

        public async Task CreateTaskAsync(CreateTaskDto createTask)
        {
            var analyst = await _employeeRepository.GetByIdAsync(createTask.AnalystId);
            if (analyst == null)
                throw new Exception("Seçilen analist bulunamadı!");

            var operationType = await _operationTypeRepository.GetByIdAsync(createTask.OperationTypeId);
            if (operationType == null)
                throw new Exception("Seçilen işlem tipi bulunamadı!");

            var task = new EntityTask
            {
                Title = createTask.Title,
                Description = createTask.Description,
                AnalystId = createTask.AnalystId,
                OperationTypeId = createTask.OperationTypeId,
                Status = AssignmentStatus.Pending,
                CreatedDate = DateTime.Now
            };

            await _taskRepository.AddAsync(task);
        }

        public async Task<IEnumerable<TaskDto>> GetUnassignedTasksAsync()
        {
            var tasks = await _taskRepository.GetUnassignedTasksAsync();

            var taskDtos = new List<TaskDto>();
            foreach (var task in tasks)
                taskDtos.Add(await MapToDto(task));

            return taskDtos;
        }

        public async Task<IEnumerable<TaskDto>> GetAssignedTasksAsync()
        {
            var tasks = await _taskRepository.GetAssignedTasksAsync();

            var taskDtos = new List<TaskDto>();
            foreach (var task in tasks)
                taskDtos.Add(await MapToDto(task));

            return taskDtos;
        }

        public async Task UpdateTaskAsync(Models.Entities.Task task)
        {
            var existingTask = await _taskRepository.GetByIdAsync(task.Id);

            if (existingTask != null)
            {
                existingTask.DeveloperId = task.DeveloperId;
                existingTask.AssignedDate = task.AssignedDate;
                existingTask.Status = task.Status;

                 await _taskRepository.Update(existingTask);
            }
        }

        public async Task<IEnumerable<Models.Entities.Task>> GetUnassignedTaskEntitiesAsync()
        {
            return await _taskRepository.GetUnassignedTasksAsync();
        }

        public async Task<IEnumerable<Models.Entities.Task>> GetAssignedTaskEntitiesAsync()
        {
            return await _taskRepository.GetAssignedTasksAsync();
        }
    }
}
