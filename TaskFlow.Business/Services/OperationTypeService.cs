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
}


