using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppCurd2.Models;

namespace WebAppCurd2.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (SampleDbContext db = new SampleDbContext())
            {
                var v = db.Employees.ToList();
                return Json(new { data = v }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddorEdit(int id)
        {
            if (id == 0)
            {
                return View(new Employee());
            }
            else
            {
                using (SampleDbContext db = new SampleDbContext())
                {
                    return View(db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault());
                }
                    
            }

        }
        [HttpPost]
        public ActionResult AddorEdit(Employee emp)
        {
              using (SampleDbContext db = new SampleDbContext())
                {
                  if (emp.EmployeeId == 0)
                  {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                  }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
           
             
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (SampleDbContext db = new SampleDbContext())
            {
               Employee emp= db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}