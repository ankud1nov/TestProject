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
        [ForeignKey(nameof(DepartmentId))]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        [NotMapped]
        public string FullName
        {
            get => $"{Surname} {Name.Substring(0, 1)}. {Lastname.Substring(0, 1)}.";
            // Значение не для записи, set - чтобы работал MVVM без ошибок.
            set { }
        }
    }
}
