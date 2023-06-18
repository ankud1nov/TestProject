using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime EmploymentDate { get; set; }
        [ForeignKey(nameof(DepartamentId))]
        public int DepartamentId { get; set; }
        public virtual Department Department { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
    }
}
