using FinalApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalApp.Controllers
{
    public class BEController : Controller
    {
        dypEntities db = new dypEntities();
        // GET: BE
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmp()
        {
            using (dypEntities db = new dypEntities())
            {
                var names = db.BEs.ToList();
                return Json(new { data = names }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Edit(int Id)
        {
            var names = db.BEs.Where(a => a.id == Id).FirstOrDefault();
            return Json(names,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(int Id)
        {
            bool status = false;
            var names= db.BEs.FirstOrDefault(a=>a.id == Id);

            if(names!=null)
            {
                db.BEs.Remove(names);
                db.SaveChanges();
                status= true;
            }
            return new JsonResult { Data = new { status = status } };
        }


        public ActionResult save(BE emp)
        {
             bool status=false;

            if(ModelState.IsValid)
            {
                using (dypEntities db=new dypEntities())
                {
                    if (emp.id > 0)
                    {
                        var v = db.BEs.Where(a => a.id == emp.id).FirstOrDefault();
                        if (v != null)
                        {
                            v.FirstName = emp.FirstName;
                            v.LastName = emp.LastName;
                            v.Email = emp.Email;
                            v.City = emp.City;
                            db.Entry(v).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        db.BEs.Add(emp);

                    }
                        db.SaveChanges();
                        status = true;
                    }
                   
                }
                
            return new JsonResult
            {
                Data = new { status = status }
            };

        }   

    }
}