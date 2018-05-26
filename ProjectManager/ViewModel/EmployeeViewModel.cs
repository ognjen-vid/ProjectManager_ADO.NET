using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManager.Models;

namespace ProjectManager.ViewModel
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public int SelectedFirmId { get; set; }
        public IEnumerable<Firm> Firms { get; set; }
        

    }
}