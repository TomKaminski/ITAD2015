using System.Web.Mvc;
using Itad2015.Contract.Service.Entity;

namespace Itad2015.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGuestService _guestService;

        public HomeController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}