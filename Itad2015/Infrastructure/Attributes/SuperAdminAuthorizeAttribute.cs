using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Itad2015.Infrastructure.Attributes
{
    public class SuperAdminAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User is ClaimsPrincipal)
            {
                var user = httpContext.User as ClaimsPrincipal;
                var isSuperAdmin = user.Claims.FirstOrDefault(c => c.Type == "isSuperAdmin");
                return isSuperAdmin != null && Convert.ToBoolean(isSuperAdmin.Value);
            }
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary(
                         new
                         {
                             area = "Admin",
                             controller = "Home",
                             action = "Index",
                             ReturnUrl = filterContext.HttpContext.Request.Url.PathAndQuery
                         })
                     );
        }
    }
}