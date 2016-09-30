using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profiles.DAL;
using Profiles.Models;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Profiles.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        ProfilesContext db = new ProfilesContext();
        public ActionResult Index()
        {
            //check if had login 
            if (Session["user"] != null)
                return new RedirectResult(string.Format("/+{0}", (Session["user"] as Profile).Name));
            return View(new Login() { UserName = "18501378365", Password = "123" });
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "UserName,Password")]Login login)
        {
            //check if had login 
            if (Session["user"] != null)
                return RedirectToAction("Index", "Profile");

            string sql = "Select * from Profile where Name=@UserName or Email =@UserName or Phone =@UserName;";
            //sql
            /*var param = new SqlParameter[] { 
                new SqlParameter("UserName",login.UserName)
            };*/
            //mysql
            var param = new MySqlParameter[] {
                new MySqlParameter("UserName",login.UserName)
            };
            
            var profile = db.Profile.SqlQuery(sql, param).FirstOrDefault();
            if (profile == null)
            {
                ViewBag.UserNameMsg = "User Name is not exisits";
                return View();
            }
            string pass = Common.Common.encryptPass(login.Password);
            if (!pass.Equals(profile.Password))
            {
                ViewBag.PasswordMsg = "Password is incorrect!";
                return View();
            }

            //login success
            Session["user"] = profile;
            //return View("~/Views/Profile/Index.cshtml",db.Profile.ToList());
            return new RedirectResult(string.Format("/+{0}", profile.Name));
        }

    }
}
