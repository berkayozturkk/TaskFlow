using TaskFlow.Models.Base;

namespace TaskFlow.Models.Entities;

public class Role : BaseEntity
{
    public Role() { }

    public string Name { get; set; }

    public ICollection<Employee>? Employees { get; set; }
}
