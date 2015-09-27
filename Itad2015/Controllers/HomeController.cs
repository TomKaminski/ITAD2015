using System;
using System.Web.Mvc;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
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
            _guestService.Create(new GuestPostDto
            {
                RegistrationTime = DateTime.Now,
                Email = "adsdaad@wp.pl",
                LastName = "Tomasz",
                FirstName = "adsads",
                SchoolName = "adsasddas"
            });
            return View();
        }
    }
}