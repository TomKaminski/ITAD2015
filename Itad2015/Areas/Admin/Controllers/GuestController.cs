using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Itad2015.Contract.Service.Entity;

namespace Itad2015.Areas.Admin.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        // GET: Admin/Guest
        public ActionResult Index()
        {
            var registeredGuests = _guestService.GetAll(x => !x.Cancelled);

            return View();
        }
    }
}