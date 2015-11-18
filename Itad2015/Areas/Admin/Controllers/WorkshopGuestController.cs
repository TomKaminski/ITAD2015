using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.Service.Entity;
using Itad2015.Helpers.Email;
using Itad2015.Hubs;
using Itad2015.ViewModels.Email;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Itad2015.Contract.DTO.Base;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Microsoft.Ajax.Utilities;

namespace Itad2015.Areas.Admin.Controllers
{
    public class WorkshopGuestController : AdminBaseController
    {
        private readonly IWorkshopGuestService _workshopGuestService;
        private readonly IWorkshopService _workshopService;
        private readonly IGuestService _guestService;

        public WorkshopGuestController(IWorkshopGuestService workshopGuestService, IWorkshopService workshopService, IGuestService guestService)
        {
            _workshopGuestService = workshopGuestService;
            _workshopService = workshopService;
            _guestService = guestService;
        }

        [HttpGet]
        public ActionResult DeleteGuest(int id)
        {
            return View(Mapper.Map<WorkshopGuestExtendedViewModel>(_workshopGuestService.GetExtendedWorkshopGuest(id).Result));
        }

        [HttpPost]
        [ActionName("DeleteGuest")]
        public ActionResult DeleteGuestPost(int id)
        {
            _workshopGuestService.Delete(id);
            return RedirectToAction("WorkshopGuestsList", "Workshop");
        }

        public JsonResult GetAll()
        {
            var registeredGuests = _guestService.GetAll(x => !x.Cancelled && x.ConfirmationTime != null && x.WorkshopGuestId != null);
            var mappedModel = registeredGuests.Result.Select(Mapper.Map<AdminGuestViewModel>).ToList();
            return Json(mappedModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendWorkshopInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SendWorkshopInfoEmails(string connectionId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<QrHub>();
            await context.Groups.Add(connectionId, connectionId);

            var data = _workshopGuestService.GetExtendedWorkshopGuests().Result.Where(x => x.Guest.AgendaEmailSent != true).Take(20);
            var workshops = _workshopService.GetAll().Result.ToList();

            var guestsToEdit = new List<GuestPostDto>();

            foreach (var d in data)
            {
                var workshop = workshops.Single(x => x.Id == d.WorkshopId);

                await new EmailHelper<WorkshopInfoEmail>(new WorkshopInfoEmail(d.Guest.Email, "reset@ath.bielsko.pl",
                    "ITAD 2015 - Warsztaty")
                {
                    WorkshopHour = workshop.Date.ToShortTimeString(),
                    WorkshopName = workshop.Title,
                    WorkshopRoom = workshop.Room,
                    Name = d.Guest.FirstName,
                    TutorName = workshop.TutorName
                })
                .SendEmailAsync();

                context.Clients.Group(connectionId).notifyEmailSent(d.Guest.Email);

                d.Guest.AgendaEmailSent = true;
                guestsToEdit.Add(Mapper.Map<GuestGetDto, GuestPostDto>(d.Guest));
                Thread.Sleep(500);
            }
            _guestService.EditMany(guestsToEdit);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}