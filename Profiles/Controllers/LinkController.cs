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
    public class LinkController : Controller
    {
        private ProfilesContext db = new ProfilesContext();

        //
        // GET: /Link/

        public ActionResult Index()
        {
            int pid = Common.Common.getProfile(Session).ID;
            return View(db.Link.Where(l => l.PID == pid).ToList());
        }

        //
        // GET: /Link/Details/5

        public ActionResult Details(int id = 0)
        {
            Link link = db.Link.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // GET: /Link/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Link/Create

        [HttpPost]
        public ActionResult Create(Link link)
        {
            if (ModelState.IsValid)
            {
                int pid = Common.Common.getProfile(Session).ID;
                link.PID = pid;
                db.Link.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(link);
        }

        //
        // GET: /Link/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Link link = db.Link.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // POST: /Link/Edit/5

        [HttpPost]
        public ActionResult Edit(Link link)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(link).State = EntityState.Modified;
                Common.Common.UpdateExcluded(db, link, l => l.PID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(link);
        }

        //
        // GET: /Link/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Link link = db.Link.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // POST: /Link/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Link link = db.Link.Find(id);
            db.Link.Remove(link);
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