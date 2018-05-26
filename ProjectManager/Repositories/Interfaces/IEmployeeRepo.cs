using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;
using ProjectManager.ViewModel;

namespace ProjectManager.Repositories.Interfaces
{
    interface IEmployeeRepo
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        bool Create(Employee employee);
        bool Update(int id, Employee employee);
        void Delete(int id);
        IEnumerable<Employee> GetByFirmId(int firmId);
        IEnumerable<Employee> GetByProjectId(int projectId);
    }
}
