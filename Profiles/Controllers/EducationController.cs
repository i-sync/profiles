using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profiles.Models;
using Profiles.DAL;
using Profiles.Common;

namespace Profiles.Controllers
{
    [CustomAuthorizeAttribute]
    public class EducationController : Controller
    {
        private ProfilesContext db = new ProfilesContext();

        //
        // GET: /Education/

        public ActionResult Index()
        {
            int pid = Common.Common.getProfile(Session).ID;
            return View(db.Education.Where(e => e.PID == pid).ToList());
        }

        //
        // GET: /Education/Details/5

        public ActionResult Details(int id = 0)
        {
            Education education = db.Education.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        //
        // GET: /Education/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Education/Create

        [HttpPost]
        public ActionResult Create(Education education)
        {
            if (ModelState.IsValid)
            {
                int pid = Common.Common.getProfile(Session).ID;
                education.PID = pid;
                db.Education.Add(education);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(education);
        }

        //
        // GET: /Education/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Education education = db.Education.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        //
        // POST: /Education/Edit/5

        [HttpPost]
        public ActionResult Edit(Education education)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(education).State = EntityState.Modified;
                Common.Common.UpdateExcluded(db, education, e => e.ID, e => e.PID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(education);
        }

        //
        // GET: /Education/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Education education = db.Education.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        //
        // POST: /Education/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Education education = db.Education.Find(id);
            db.Education.Remove(education);
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