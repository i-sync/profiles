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
    public class ExperienceController : Controller
    {
        private ProfilesContext db = new ProfilesContext();

        //
        // GET: /Experience/

        public ActionResult Index()
        {
            int pid = Common.Common.getProfile(Session).ID;
            return View(db.Experience.Where(e => e.PID == pid).ToList());
        }

        //
        // GET: /Experience/Details/5

        public ActionResult Details(int id = 0)
        {
            Experience experience = db.Experience.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        //
        // GET: /Experience/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Experience/Create

        [HttpPost]
        public ActionResult Create(Experience experience)
        {
            if (ModelState.IsValid)
            {
                int pid = Common.Common.getProfile(Session).ID;
                experience.PID = pid;
                db.Experience.Add(experience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(experience);
        }

        //
        // GET: /Experience/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Experience experience = db.Experience.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        //
        // POST: /Experience/Edit/5

        [HttpPost]
        public ActionResult Edit(Experience experience)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(experience).State = EntityState.Modified;
                Common.Common.UpdateExcluded(db, experience, e => e.ID, e => e.PID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(experience);
        }

        //
        // GET: /Experience/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Experience experience = db.Experience.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        //
        // POST: /Experience/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Experience experience = db.Experience.Find(id);
            db.Experience.Remove(experience);
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