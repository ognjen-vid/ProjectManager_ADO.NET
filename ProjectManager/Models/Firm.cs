using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class Firm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}