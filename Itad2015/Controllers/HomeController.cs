using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Itad2015.Contract.Service.Entity;
using Itad2015.ViewModels;
using Itad2015.ViewModels.Base;
using Itad2015.ViewModels.Home;

namespace Itad2015.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGuestService _guestService;
        private readonly IWorkshopService _workshopService;
        private readonly IWorkshopGuestService _workshopGuestService;


        private const int NormalTicketsCount = 350;

        public HomeController(IGuestService guestService, IWorkshopService workshopService, IWorkshopGuestService workshopGuestService)
        {
            _guestService = guestService;
            _workshopService = workshopService;
            _workshopGuestService = workshopGuestService;
        }

        // GET: Home
        public ActionResult Index(List<AlertViewModel> alerts = null)
        {
            var workshops = _workshopService.GetAll().Result.Select(Mapper.Map<WorkshopDropdownViewModel>).ToList();

            var workshopGuestsGrouped =
                _workshopGuestService.GetAll()
                    .Result.GroupBy(x => x.WorkshopId)
                    .Select(x => new { Id = x.Key, Count = x.Count() }).ToList();

            foreach (var workshop in workshops)
            {
                var firstOrDefault = workshopGuestsGrouped.FirstOrDefault(x => x.Id == workshop.Id);
                workshop.LeftParticipants = workshop.MaxParticipants - firstOrDefault?.Count ?? workshop.MaxParticipants;
            }

            var normalTicketsLeft = NormalTicketsCount - _guestService.Count();

            return View(new IndexViewModel
            {
                IndexGuestModel = new IndexGuestModel
                {
                    NormalTicketsLeft = normalTicketsLeft
                },
                IndexWorkshopGuestModel = new IndexWorkshopGuestModel
                {
                    WorkshopDropdownList = workshops
                },
                Alerts = alerts ?? new List<AlertViewModel>()
            });
        }

        public ActionResult DummyRedirectAlerts()
        {
            var alerts = new List<AlertViewModel>
            {
                new AlertViewModel
                {
                    AlertText = "Tutaj mamy error text sad asd sa das dasd as das dasd as",
                    AlertClass = "alert-danger"
                },
                new AlertViewModel
                {
                    AlertText = "Tutaj mamy success text adsasddasdas as das das das das ",
                    AlertClass = "alert-success"
                }
            };
            var workshops = _workshopService.GetAll().Result.Select(Mapper.Map<WorkshopDropdownViewModel>).ToList();

            var workshopGuestsGrouped =
                _workshopGuestService.GetAll()
                    .Result.GroupBy(x => x.WorkshopId)
                    .Select(x => new { Id = x.Key, Count = x.Count() }).ToList();

            foreach (var workshop in workshops)
            {
                var firstOrDefault = workshopGuestsGrouped.FirstOrDefault(x => x.Id == workshop.Id);
                workshop.LeftParticipants = workshop.MaxParticipants - firstOrDefault?.Count ?? workshop.MaxParticipants;
            }

            var normalTicketsLeft = NormalTicketsCount - _guestService.Count();

            return View("Index",new IndexViewModel
            {
                IndexGuestModel = new IndexGuestModel
                {
                    NormalTicketsLeft = normalTicketsLeft
                },
                IndexWorkshopGuestModel = new IndexWorkshopGuestModel
                {
                    WorkshopDropdownList = workshops
                },
                Alerts = alerts
            });
        }
    }
}