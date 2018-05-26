using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Repositories;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class FirmController : Controller
    {
        IFirmRepo firmRepo = new FirmRepo();
        IEmployeeRepo employeeRepo = new EmployeeRepo();
        IProjectRepo projectRepo = new ProjectRepo();

        public ActionResult Index()
        {
            var firms = firmRepo.GetAll();
            return View(firms);
        }

        public ActionResult Details(int id)
        {
            Firm firm = firmRepo.GetById(id);
            return View(firm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Firm firm)
        {
            try
            {
                firmRepo.Create(firm);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(firm);
            }
        }

        public ActionResult Edit(int id)
        {
            Firm firm = firmRepo.GetById(id);
            return View(firm);
        }

        [HttpPost]
        public ActionResult Edit(int id, Firm firm)
        {
            try
            {
                firmRepo.Update(id, firm);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(firm);
            }
        }

        public ActionResult Delete(int id)
        {
            Firm firm = firmRepo.GetById(id);
            return View(firm);
        }

        [HttpPost]
        public ActionResult Delete(int id, Firm firm)
        {
            try
            {
                firmRepo.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(firm);
            }
        }

        public ActionResult FirmEmployees(int firmId)
        {
            var firmEmployees = employeeRepo.GetByFirmId(firmId);
            return View(firmEmployees);
        }

        public ActionResult FirmProjects(int firmId)
        {
            var firmProjects = projectRepo.GetByFirmId(firmId);
            return View(firmProjects);
        }
    }
}
