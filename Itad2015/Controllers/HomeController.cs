using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using DevTrends.MvcDonutCaching;
using Itad2015.Contract.Service.Entity;
using Itad2015.ViewModels;
using Itad2015.ViewModels.Base;
using Itad2015.ViewModels.Guest;
using Itad2015.ViewModels.Home;

namespace Itad2015.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGuestService _guestService;
        private readonly IWorkshopService _workshopService;
        private readonly IWorkshopGuestService _workshopGuestService;

        public HomeController(IGuestService guestService, IWorkshopService workshopService, IWorkshopGuestService workshopGuestService)
        {
            _guestService = guestService;
            _workshopService = workshopService;
            _workshopGuestService = workshopGuestService;
        }

        //[DonutOutputCache(CacheProfile = "1Day", Location = OutputCacheLocation.Server)]
        public ActionResult Index(List<AlertViewModel> alerts = null)
        {
            return View("Index",new IndexViewModel
            {
                RegisterWorkshopGuestViewModel = new RegisterWorkshopGuestViewModel(),
                RegisterGuestViewModel = new RegisterGuestViewModel(),
                Alerts = alerts ?? new List<AlertViewModel>()
            });
        }

        [ChildActionOnly]
        public PartialViewResult WorkshopRegisterListSection()
        {
            var workshops = _workshopService.GetAll().Result.OrderBy(x=>x.Title).Select(Mapper.Map<WorkshopDropdownViewModel>).ToList();

            var workshopGuestsGrouped =
                _workshopGuestService.GetAll()
                    .Result.GroupBy(x => x.WorkshopId)
                    .Select(x => new { Id = x.Key, Count = x.Count() }).ToList();

            foreach (var workshop in workshops)
            {
                var firstOrDefault = workshopGuestsGrouped.FirstOrDefault(x => x.Id == workshop.Id);
                workshop.LeftParticipants = workshop.MaxParticipants - firstOrDefault?.Count ?? workshop.MaxParticipants;
            }

            return PartialView(workshops);
        }

        [HttpGet]
        public ActionResult ConfirmRegistration(int id, string confirmationCode)
        {
            var result = _guestService.ConfirmRegistration(id, confirmationCode);
            var alertModel = new AlertViewModel();
            if (result.FirstResult)
            {
                alertModel.AlertClass = "alert-success";
                alertModel.AlertText =
                    $"<img src='/Content/images/Mail/tick_green.png' /><span style='padding-left:20px;'>Rejestracja przebiegła pomyślnie. Jesteś naszym {result.SecondResult} gościem. Dziękujemy!</span>";

            }
            else
            {
                alertModel.AlertClass = "alert-danger";
                alertModel.AlertText =
                    $"<img src='/Content/images/Mail/cancel_red.png' /><span style='padding-left:20px;'>Wystąpił błąd podczas procesu rejestracji ({result.ValidationErrors.FirstOrDefault()}). Spróbuj ponownie lub skontaktuj się z administratorem strony</span>";
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
                alertModel.AlertText =
                    @"<img src='/Content/images/Mail/tick_green.png' /><span style='padding-left:20px;'>Rejestracja została ANULOWANA pomyślnie!</span>";
            }
            else
            {
                alertModel.AlertClass = "alert-danger";
                alertModel.AlertText = $"<img src='/Content/images/Mail/cancel_red.png' /><span style='padding-left:20px;'>Wystąpił błąd podczas procesu rejestracji ({result.ValidationErrors.FirstOrDefault()}). Spróbuj ponownie lub skontaktuj się z administratorem strony</span>";

            }
            return Index(new List<AlertViewModel> { alertModel });
        }


        //Home child actions
        [ChildActionOnly]
        public PartialViewResult UpperSection()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult LowerSection()
        {
            return PartialView();
        }


        [ChildActionOnly]
        public PartialViewResult RegisteredGuestsPartialCount()
        {
            return PartialView(new RegisteredGuestsCountViewModel
            {
                MaxGuests = _guestService.MaxNormalRegisteredGuests,
                RegisteredGuests = _guestService.Count(x => !x.Cancelled && x.WorkshopGuestId == null)
            });
        }
    }
}