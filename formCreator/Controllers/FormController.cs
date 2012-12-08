using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using formCreator.Models;

namespace formCreator.Controllers
{ 
    public class FormController : BaseController
    {
        private FormDBContext db = new FormDBContext();

        //
        // GET: /Form/

        public ViewResult Index()
        {
            var forms = RavenSession.Query<Form>().ToList();
            return View(forms);
        }

        //
        // GET: /Form/Details/5

        public ViewResult Details(int id)
        {
            Form form = (Form)RavenSession.Query<Form>().Where(f => f.Id == id).First();
            return View(form);
        }

        //
        // GET: /Form/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Form/Create

        [HttpPost]
        public ActionResult Create(Form form)
        {
            if (ModelState.IsValid)
            {

                List<Models.Attribute> attributes = new List<Models.Attribute>();
                String given_atts = Request["formAttributes"];
                string[] atts = given_atts.Split(',');

                foreach (string little_att in atts)
                {
                    attributes.Add(new Models.Attribute { Name = little_att, Value = little_att });
                }

                form.Attributes = attributes;

                RavenSession.Store(form);
                RavenSession.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(form);
        }
        
        //
        // GET: /Form/Edit/5
 
        public ActionResult Edit(int id)
        {
            Form form = db.Forms.Find(id);
            return View(form);
        }

        //
        // POST: /Form/Edit/5

        [HttpPost]
        public ActionResult Edit(Form form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(form).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        //
        // GET: /Form/Delete/5
 
        public ActionResult Delete(int id)
        {
            Form form = db.Forms.Find(id);
            return View(form);
        }

        //
        // POST: /Form/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Form form = db.Forms.Find(id);
            db.Forms.Remove(form);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}