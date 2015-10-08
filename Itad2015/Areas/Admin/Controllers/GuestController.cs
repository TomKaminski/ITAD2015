using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.Service.Entity;

namespace Itad2015.Areas.Admin.Controllers
{
    public class GuestController : AdminBaseController
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        // GET: Admin/Guest
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {
            var registeredGuests = _guestService.GetAll(x => !x.Cancelled);

            var mappedModel = registeredGuests.Result.Select(Mapper.Map<AdminGuestViewModel>).ToList();
            return Json(mappedModel,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CheckIn(int id)
        {
            var result = _guestService.CheckIn(id);
            return Json(new
            {
                status = result.Result,
                errors = result.ValidationErrors??new List<string>()
            });
        }
        [HttpPost]
        public JsonResult CheckOut(int id)
        {
            var result = _guestService.CheckOut(id);
            return Json(new
            {
                status = result.Result,
                errors = result.ValidationErrors ?? new List<string>()
            });
        }
    }
}