using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Firm Firm { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public override bool Equals(object obj)
        {
            var project = obj as Project;
            if (project == null)
                return false;

            return this.Id == project.Id;
        }
    }
}