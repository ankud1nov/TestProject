using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Domain.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual DepartmentHead? DepartmentHead { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
