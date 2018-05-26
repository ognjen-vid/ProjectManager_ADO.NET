using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Repositories;
using ProjectManager.Models;
using ProjectManager.ViewModel;

namespace ProjectManager.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepo employeeRepo = new EmployeeRepo();
        private IFirmRepo firmRepo = new FirmRepo();
        private IProjectRepo projectRepo = new ProjectRepo();
        
        public ActionResult Index()
        {
            var employees = employeeRepo.GetAll();
            return View(employees);
        }

        public ActionResult Details(int id)
        {
            Employee emp = employeeRepo.GetById(id);
            return View(emp);
        }

        public ActionResult Create()
        {
            EmployeeViewModel evm = new EmployeeViewModel();
            evm.Firms = firmRepo.GetAll();
            return View(evm);
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel evm)
        {
            try
            {
                Employee emp = evm.Employee;
                emp.Firm = firmRepo.GetById(evm.SelectedFirmId);
                employeeRepo.Create(emp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(evm);
            }
        }

        public ActionResult Edit(int id)
        {
            EmployeeViewModel evm = new EmployeeViewModel();
            evm.Firms = firmRepo.GetAll();
            evm.Employee = employeeRepo.GetById(id);
            evm.SelectedFirmId = evm.Employee.Firm.Id;
            return View(evm);
        }

        [HttpPost]
        public ActionResult Edit(int id, EmployeeViewModel evm)
        {
            try
            {
                Employee emp = evm.Employee;
                emp.Firm = firmRepo.GetById(evm.SelectedFirmId);
                employeeRepo.Update(id, emp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(evm);
            }
        }

        public ActionResult Delete(int id)
        {
            Employee emp = employeeRepo.GetById(id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                employeeRepo.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(employee);
            }
        }

        public ActionResult EmployeeProjects(int employeeId)
        {
            var projects = projectRepo.GetByEmployeeId(employeeId);
            return View(projects);
        }
    }
}
