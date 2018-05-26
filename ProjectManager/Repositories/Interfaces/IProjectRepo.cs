using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;

namespace ProjectManager.Repositories.Interfaces
{
    interface IProjectRepo
    {
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        bool Create(Project project);
        bool Update(int id, Project project);
        void Delete(int id);
        IEnumerable<Project> GetByFirmId(int firmId);
        IEnumerable<Project> GetByEmployeeId(int employeeId);
        bool AddToProject(int SelectedEmployeeId, int projectId);
        bool RemoveFromProject(int employeeId, int projectId);
    }
}
