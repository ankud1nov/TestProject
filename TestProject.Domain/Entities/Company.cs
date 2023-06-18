using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TestProject.Domain.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string JuridicalAddress { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
