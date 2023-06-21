using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Domain.Entities;

public class DepartmentHead
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(DepartmentId))]
    public virtual Department Department { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public virtual Employee EmployeeHead { get; set;}
}
