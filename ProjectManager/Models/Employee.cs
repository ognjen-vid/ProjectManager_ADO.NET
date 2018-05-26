using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public Firm Firm { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();

      
    }
}