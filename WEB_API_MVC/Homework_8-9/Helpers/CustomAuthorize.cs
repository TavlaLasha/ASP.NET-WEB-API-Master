using System;
using System.Linq;
using System.Security.Claims;
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

            var User = SessionAssistance.GetUser(httpContext.Session);
            if (string.IsNullOrEmpty(User?.access_token))
            {
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    var roles = Roles.Split(',').Select(r => r.Trim());
                    if (User.Roles == null || User.Roles?.Count == 0 || (User.Roles?.Count > 0 && !roles.Any(User.Roles.Contains)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }


            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
            {
                if (AuthorizeCore(filterContext.HttpContext))
                {
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Account/Login");
                }
            }
        }
    }

    public class AppPrincipal : ClaimsPrincipal
    {
        public override bool IsInRole(string role)
        {
            return true;
        }
    }
}