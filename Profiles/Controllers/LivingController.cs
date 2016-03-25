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
    public class LivingController : Controller
    {
        private ProfilesContext db = new ProfilesContext();

        //
        // GET: /Living/

        public ActionResult Index()
        {
            int pid = Common.Common.getProfile(Session).ID;
            return View(db.Living.Where(l => l.PID == pid).ToList());
        }

        //
        // GET: /Living/Details/5

        public ActionResult Details(int id = 0)
        {
            Living living = db.Living.Find(id);
            if (living == null)
            {
                return HttpNotFound();
            }
            return View(living);
        }

        //
        // GET: /Living/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Living/Create

        [HttpPost]
        public ActionResult Create(Living living)
        {
            if (ModelState.IsValid)
            {
                int pid = Common.Common.getProfile(Session).ID;
                living.PID = pid;
                db.Living.Add(living);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(living);
        }

        //
        // GET: /Living/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Living living = db.Living.Find(id);
            if (living == null)
            {
                return HttpNotFound();
            }
            return View(living);
        }

        //
        // POST: /Living/Edit/5

        [HttpPost]
        public ActionResult Edit(Living living)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(living).State = EntityState.Modified;
                Common.Common.UpdateExcluded(db, living, l => l.ID, l => l.PID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(living);
        }

        //
        // GET: /Living/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Living living = db.Living.Find(id);
            if (living == null)
            {
                return HttpNotFound();
            }
            return View(living);
        }

        //
        // POST: /Living/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Living living = db.Living.Find(id);
            db.Living.Remove(living);
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