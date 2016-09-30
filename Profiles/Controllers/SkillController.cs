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
using System.Data.Objects;

namespace Profiles.Controllers
{
    [CustomAuthorizeAttribute]
    public class SkillController : Controller
    {
        private ProfilesContext db = new ProfilesContext();

        //
        // GET: /Skill/

        public ActionResult Index()
        {
            int pid = Common.Common.getProfile(Session).ID;
            return View(db.Skill.SqlQuery(string.Format("select * from Skill where PID = {0}",pid)));
        }

        //
        // GET: /Skill/Details/5

        public ActionResult Details(int id = 0)
        {
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        //
        // GET: /Skill/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Skill/Create

        [HttpPost]
        public ActionResult Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                int pid = Common.Common.getProfile(Session).ID;
                skill.PID = pid;
                db.Skill.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        //
        // GET: /Skill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        //
        // POST: /Skill/Edit/5

        [HttpPost]
        public ActionResult Edit(Skill skill)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(skill).State = EntityState.Modified;
                Common.Common.UpdateExcluded(db, skill, s => s.PID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skill);
        }

        //
        // GET: /Skill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        //
        // POST: /Skill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skill.Find(id);
            db.Skill.Remove(skill);
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