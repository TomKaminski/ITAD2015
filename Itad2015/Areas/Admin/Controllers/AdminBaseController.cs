using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Itad2015.Controllers;
using Itad2015.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Itad2015.Areas.Admin.Controllers
{
    [Authorize]
    public abstract class AdminBaseController : BaseController
    {
        protected AppUserState AppUserState = new AppUserState();

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var appUserState = new AppUserState();
            if (User is ClaimsPrincipal)
            {
                var user = User as ClaimsPrincipal;
                var claims = user.Claims.ToList();
                var email = GetClaim(claims, ClaimTypes.NameIdentifier);

                appUserState.Email = email;
            }
            AppUserState = appUserState;
            ViewBag.UserState = AppUserState;
        }

        private static string GetClaim(IEnumerable<Claim> claims, string key)
        {
            var claim = claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        protected void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
    }
}