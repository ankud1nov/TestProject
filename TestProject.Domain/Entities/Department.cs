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
        [ForeignKey(nameof(DepartmentHeadId))]
        public int DepartmentHeadId { get; set; }
        public virtual Employee DepartmentHead { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
