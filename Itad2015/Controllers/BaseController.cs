using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace Itad2015.Controllers
{
    [RequireHttps]
    public class BaseController : Controller
    {
        protected string RenderViewToString(string controllerName, string viewName, object viewData)
        {
            using (var writer = new StringWriter())
            {
                var routeData = new RouteData();
                routeData.Values.Add("controller", controllerName);
                var razorViewEngine = new RazorViewEngine();
                var razorViewResult = razorViewEngine.FindView(ControllerContext, viewName, "", false);

                var viewContext = new ViewContext(ControllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);
                razorViewResult.View.Render(viewContext, writer);
                return writer.ToString();
            }
        } 
    }
}