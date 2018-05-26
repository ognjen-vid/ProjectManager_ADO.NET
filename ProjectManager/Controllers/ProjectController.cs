using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.ViewModel;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Repositories;
using ProjectManager.Models;
using System.Diagnostics;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        IProjectRepo projectRepo = new ProjectRepo();
        IFirmRepo firmRepo = new FirmRepo();
        IEmployeeRepo employeeRepo = new EmployeeRepo();

        public ActionResult Index()
        {
            var projects = projectRepo.GetAll();
            return View(projects);
        }

        public ActionResult Details(int id)
        {
            Project project = projectRepo.GetById(id);
            return View(project);
        }

        public ActionResult Create()
        {
            ProjectViewModel pvm = new ProjectViewModel();
            pvm.Firms = firmRepo.GetAll();
            return View(pvm);
        }

        [HttpPost]
        public ActionResult Create(ProjectViewModel pvm)
        {
            try
            {
                Project project = pvm.Project;
                project.Firm = firmRepo.GetById(pvm.SelectedFirmId);
                projectRepo.Create(project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pvm);
            }
        }

        public ActionResult Edit(int id)
        {
            ProjectViewModel pvm = new ProjectViewModel();
            pvm.Firms = firmRepo.GetAll();
            pvm.Project = projectRepo.GetById(id);
            pvm.SelectedFirmId = pvm.Project.Firm.Id;
            return View(pvm);
        }

        [HttpPost]
        public ActionResult Edit(int id, ProjectViewModel pvm)
        {
            try
            {
                Project project= pvm.Project;
                project.Firm = firmRepo.GetById(pvm.SelectedFirmId);
                projectRepo.Update(id, project);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(pvm);
            }
        }

        public ActionResult Delete(int id)
        {
            Project project = projectRepo.GetById(id);
            return View(project);
        }

        [HttpPost]
        public ActionResult Delete(int id, Project project)
        {
                projectRepo.Delete(id);
                return RedirectToAction("Index");
        }

        public ActionResult ActiveEmployees(int id)
        {
            var activeEmployees = employeeRepo.GetByProjectId(id);
            ViewBag.ProjectId = id;
            ViewBag.FirmId = projectRepo.GetById(id).Firm.Id;
            return View(activeEmployees);
        }

        public ActionResult GetNotActive(int projectId, int firmId)
        {
            List<Employee> allFirmIdEmployees = (List<Employee>)employeeRepo.GetByFirmId(firmId);
            List<Employee> notActive = new List<Employee>();

            Project project = projectRepo.GetById(projectId);
            ViewBag.ProjectId = projectId;
            ViewBag.FirmId = firmId;

            foreach (Employee emp in allFirmIdEmployees)
            {
                emp.Projects = (List<Project>)projectRepo.GetByEmployeeId(emp.Id);
                if(!emp.Projects.Contains(project))
                    notActive.Add(emp);
            }

            return View(notActive);
        }

        [HttpPost]
        public ActionResult AddToProject(int SelectedEmployeeId, int projectId)
        {
            projectRepo.AddToProject(SelectedEmployeeId, projectId);
            return RedirectToAction("ActiveEmployees", new { id = projectId });
        }

        [HttpPost]
        public ActionResult RemoveFromProject(int employeeId, int projectId)
        {
            projectRepo.RemoveFromProject(employeeId, projectId);
            return RedirectToAction("ActiveEmployees", new { id = projectId });
        }

    }
}