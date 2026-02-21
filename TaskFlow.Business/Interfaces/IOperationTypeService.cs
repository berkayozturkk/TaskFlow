using TaskFlow.Business.DTOs;

namespace TaskFlow.Business.Interfaces;

public interface IOperationTypeService
{
    Task<IEnumerable<OperationTypeDto>> GetAllOperationTypesAsync();
}

