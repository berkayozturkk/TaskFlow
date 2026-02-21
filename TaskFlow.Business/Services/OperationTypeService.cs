using TaskFlow.Business.DTOs;
using TaskFlow.Business.Interfaces;
using TaskFlow.Data.Repositories.Interfaces;

namespace TaskFlow.Business.Services;

public class OperationTypeService : IOperationTypeService
{
    private readonly IOperationTypeRepository _operationTypeRepository;

    public OperationTypeService(IOperationTypeRepository operationTypeRepository)
    {
        _operationTypeRepository = operationTypeRepository;
    }

    public async Task<IEnumerable<OperationTypeDto>> GetAllOperationTypesAsync()
    {
        var operationTypes = await _operationTypeRepository.GetAllAsync();

        return operationTypes.Select(o => new OperationTypeDto
        {
            Id = o.Id,
            Name = o.Name,
            Description = o.Description,
            DifficultyLevel = (int)o.DifficultyLevel
        });
    }
}


