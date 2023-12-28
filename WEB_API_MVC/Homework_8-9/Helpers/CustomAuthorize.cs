using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Homework_8_9.Helpers
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public CustomAuthorize()
        {

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var SessionAssistance = new SessionAssistance(httpContext.Session);
            var AccessToken = SessionAssistance.Get<string>("user");
            if (string.IsNullOrEmpty(AccessToken))
            {
                return false;
            }

            return true;
        }

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (filterContext == null)
        //    {
        //        throw new ArgumentNullException("filterContext");
        //    }


        //    if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
        //    {
        //        if (AuthorizeCore(filterContext.HttpContext))
        //        {
        //            filterContext.HttpContext.User.Identity. = true;
        //            return;
        //        }
        //        else
        //        {
        //            HandleUnauthorizedRequest(filterContext);
        //        }
        //    }
        //}
    }
}