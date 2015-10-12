using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Contract.Service.Entity;
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
                Alerts = alerts ?? new List<AlertViewModel>()
            });
        }

        public ActionResult DummyRedirectAlerts()
        {
            var alerts = new List<AlertViewModel>
            {
                new AlertViewModel
                {
                    AlertText = "Wystąpił błąd podczas potwierdzania rejestracji na konferencję. <br/> Spróbuj ponownie lub skontaktuj się z administratorem strony",
                    AlertClass = "alert-danger"
                },
                new AlertViewModel
                {
                    AlertText = "Rejestracja przebiegła pomyślnie. Dziękujemy!",
                    AlertClass = "alert-success"
                }
            };
            return Index(alerts);
        }

        [HttpGet]
        public ActionResult ConfirmRegistration(int id, string confirmationCode)
        {
            var result = _guestService.ConfirmRegistration(id, confirmationCode);
            var alertModel = new AlertViewModel();
            if (result.Result)
            {
                alertModel.AlertClass = "alert-success";
                alertModel.AlertText = "Rejestracja została potwierdzona pomyślnie!";
            }
            else
            {
                alertModel.AlertClass = "alert-danger";
                alertModel.AlertText = $"Wystąpił błąd: {result.ValidationErrors.FirstOrDefault()}";
            }
            return Index(new List<AlertViewModel> { alertModel });
        }

        [HttpGet]
        public ActionResult CancelRegistration(int id, string cancelationCode)
        {
            var result = _guestService.CancelRegistration(id, cancelationCode);
            var alertModel = new AlertViewModel();
            if (result.Result)
            {
                alertModel.AlertClass = "alert-success";
                alertModel.AlertText = "Rejestracja została ANULOWANA pomyślnie!";
            }
            else
            {
                alertModel.AlertClass = "alert-danger";
                alertModel.AlertText = $"Wystąpił błąd: {result.ValidationErrors.FirstOrDefault()}";
            }
            return Index(new List<AlertViewModel> { alertModel });
        }
    }
}