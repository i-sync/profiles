using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profiles.Models;
using Profiles.DAL;
using System.IO;
using Profiles.Common;
using System.Data.SqlClient;

namespace Profiles.Controllers
{
    public class ProfileController : Controller
    {
        private ProfilesContext db = new ProfilesContext();

        [HttpPost]
        public ActionResult Avatar(HttpPostedFileBase avatar)
        {
            if (avatar != null && avatar.ContentLength > 0)
            {
                //var fileName = Path.GetFileName(avatar.FileName);
                var fileName = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(avatar.FileName));
                var path = Path.Combine(Server.MapPath("~/Content/Avatar"), fileName);
                ViewBag.filePath = Path.Combine("~/Content/Avatar", fileName);
                avatar.SaveAs(path);
            }
            return View("Create");
        }

        [HttpPost]
        public ActionResult EditAvatar(int id, HttpPostedFileBase avatar)
        {
            if (id <= 0)
                return HttpNotFound();
            string filePath = string.Empty;
            if (avatar != null && avatar.ContentLength > 0)
            {
                //var fileName = Path.GetFileName(avatar.FileName);
                var fileName = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(avatar.FileName));
                var path = Path.Combine(Server.MapPath("~/Content/Avatar"), fileName);
                filePath = Path.Combine("~/Content/Avatar", fileName);
                avatar.SaveAs(path);
            }
            Profile profile = db.Profile.FirstOrDefault(p => p.ID == id);
            if (profile == null)
                return HttpNotFound();

            if (!string.IsNullOrEmpty(filePath))
            {
                profile.Avatar = filePath;
                profile.UpdateDate = DateTime.Now;
                db.Entry(profile).Property(p => p.Avatar).IsModified = true;
                db.Entry(profile).Property(p => p.UpdateDate).IsModified = true;
                //UpdatedProperties<Profile>(profile, p => p.Avatar, p => p.UpdateDate);
                db.SaveChanges();

            }
            return View("EditAvatar", profile);
        }

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            return View(db.Profile.ToList());
        }

        //
        // GET: /Profile/Details/5

        public ActionResult Details(int id = 0)
        {
            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: /Profile/tianzhenyun
        public ActionResult Info(string name)
        {
            string sql = "select * from Profile where Name = @Name";
            Profile profile = db.Profile.SqlQuery(sql, new SqlParameter("Name", name)).FirstOrDefault();
            if (profile == null)
                return HttpNotFound();
            int pid = profile.ID;
            profile.Skills = db.Skill.SqlQuery(string.Format("select * from Skill where pid = {0}",pid)).ToList();
            profile.Projects = db.Project.SqlQuery(string.Format("select * from Project where pid = {0}", pid)).ToList();
            profile.Experiences = db.Experience.SqlQuery(string.Format("select * from Experience where pid = {0}", pid)).ToList();
            profile.Educations = db.Education.SqlQuery(string.Format("select * from Education where pid = {0}", pid)).ToList();
            profile.Livings = db.Living.SqlQuery(string.Format("select * from Living where pid = {0}", pid)).ToList();
            profile.Links = db.Link.SqlQuery(string.Format("select * from Link where pid = {0}", pid)).ToList();

            return View(profile);
        }

        //
        // GET: /Profile/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Profile/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,NickName,Avatar,Email,Phone,Address,Intro,Password")]Profile profile)
        {
            if (ModelState.IsValid)
            {
                profile.Password = Common.Common.encryptPass(profile.Password);
                profile.AddDate = profile.UpdateDate = DateTime.Now;
                db.Profile.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        //
        // GET: /Profile/Edit/5
        [CustomAuthorizeAttribute]
        public ActionResult Edit(int id = 0)
        {
            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        [CustomAuthorizeAttribute]
        public ActionResult Edit([Bind(Include = "ID,Name,NickName,Email,Phone,Address,Intro")]Profile profile)
        {
            if (ModelState.IsValid)
            {
                profile.UpdateDate = DateTime.Now;
                //db.Entry(profile).State = EntityState.Modified;
                //UpdatedProperties<Profile>(profile, p => p.Name, p => p.NickName, p => p.Email, p => p.Phone, p => p.Address, p => p.UpdateDate);
                Common.Common.UpdateExcluded<Profile>(db, profile, p => p.AddDate, p => p.Avatar, p => p.Password);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(profile);
        }

        //
        // GET: /Profile/Edit/5

        [CustomAuthorizeAttribute]
        public ActionResult EditAvatar(int id = 0)
        {
            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // GET: /Profile/Delete/5
        [CustomAuthorizeAttribute]
        public ActionResult Delete(int id = 0)
        {
            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost, ActionName("Delete")]
        [CustomAuthorizeAttribute]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profile.Find(id);
            db.Profile.Remove(profile);
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