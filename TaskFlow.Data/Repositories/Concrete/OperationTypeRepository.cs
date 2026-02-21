using TaskFlow.Data.Context;
using TaskFlow.Data.Repositories.Interfaces;
using TaskFlow.Models.Entities;

namespace TaskFlow.Data.Repositories;

public class OperationTypeRepository : GenericRepository<OperationType>, IOperationTypeRepository
{
    public OperationTypeRepository(AppDbContext context) : base(context) {}
}
