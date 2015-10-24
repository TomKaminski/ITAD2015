using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.Service.Entity;
using Itad2015.Helpers.Email;
using Itad2015.ViewModels.Email;

namespace Itad2015.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IGuestService _guestService;

        public HomeController(IGuestService guestService)
        {
            _guestService = guestService;
        }


        public ActionResult SendConfirmationEmail(int id)
        {
            var guest = _guestService.Get(id);
            new EmailHelper<GuestRegisterEmail>(new GuestRegisterEmail(guest.Result.Email, "reset@ath.bielsko.pl", "Rejestracja na konferencję.")
            {
                LastName = guest.Result.LastName,
                FirstName = guest.Result.FirstName,
                ConfirmationHash = guest.Result.ConfirmationHash,
                CancelationHash = guest.Result.CancelationHash,
                Id = guest.Result.Id
            }).SendEmail();
            return RedirectToAction("Index");
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            var guests = _guestService.GetAll(x => x.ConfirmationTime == null).Result.Select(Mapper.Map<AdminGuestViewModel>).ToList();
            return View(guests);
        }
    }
}