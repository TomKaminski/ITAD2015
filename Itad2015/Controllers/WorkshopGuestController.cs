using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.Helpers.Email;
using Itad2015.ViewModels;
using Itad2015.ViewModels.Email;

namespace Itad2015.Controllers
{
    public class WorkshopGuestController : Controller
    {
        private readonly IWorkshopGuestService _workshopGuestService;

        public WorkshopGuestController(IWorkshopGuestService workshopGuestService)
        {
            _workshopGuestService = workshopGuestService;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Register(RegisterWorkshopGuestViewModel model)
        {
            return Json(new
            {
                status = false,
                errors = new List<string> { "Rejestracja na IT Academic Day jest zamknięta"}
            });
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    errors.AddRange(modelState.Errors.Select(error => error.ErrorMessage));
                }
                return Json(new
                {
                    status = false,
                    errors
                });
            }


            var mappedGuestModel = Mapper.Map<GuestPostDto>(model);
            var mappedWorkshopGuestModel = Mapper.Map<WorkshopGuestPostDto>(model);

            var res = _workshopGuestService.Register(mappedGuestModel, mappedWorkshopGuestModel);

            if (!res.ValidationErrors.Any())
            {
                var user = res.FirstResult;
                var workshop = res.SecondResult;

                new EmailHelper<WorkshopGuestRegisterEmail>(new WorkshopGuestRegisterEmail(user.Email, "reset@ath.bielsko.pl", "Rejestracja na konferencję.")
                {
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    ConfirmationHash = user.ConfirmationHash,
                    CancelationHash = user.CancelationHash,
                    SchoolName = model.SchoolName,
                    WorkshopTitle = workshop.Title,
                    Id = user.Id
                }).SendEmail();
            }

            return Json(new
            {
                status = res.ValidationErrors.Count == 0,
                errors = res.ValidationErrors
            });
        }
    }
}