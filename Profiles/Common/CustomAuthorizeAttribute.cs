using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Profiles.Common
{
    public class CustomAuthorizeAttribute :System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            } 
            //return base.AuthorizeCore(httpContext);
            if (httpContext.Session["user"] == null)
            {
                //return RedirectToAction
                httpContext.Response.StatusCode = 401;
                return false;
            }
            return true;
        }

        /*public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }*/

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 401)
                filterContext.Result = new RedirectResult("/Login/Index");
        }
    }
}