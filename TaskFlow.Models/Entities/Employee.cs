using TaskFlow.Models.Base;

namespace TaskFlow.Models.Entities;

public class Employee : BaseEntity
{
    public Employee() {}

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
}
