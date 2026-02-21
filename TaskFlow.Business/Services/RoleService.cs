using TaskFlow.Business.Interfaces;
using TaskFlow.Data.Repositories.Interfaces;

namespace TaskFlow.Business.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
}


