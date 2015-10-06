using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.ViewModels;

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
        [ValidateAntiForgeryToken]
        public JsonResult Register([Bind(Prefix = "RegisterWorkshopGuestViewModel")]RegisterWorkshopGuestViewModel model)
        {
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

                //TODO: Uncomment if mail client will be provided
                //new EmailHelper<WorkshopGuestRegisterEmail>(new WorkshopGuestRegisterEmail(user.Email, "itaddbb@gmail.com", "Rejestracja na konferencję.")
                //{
                //    LastName = user.LastName,
                //    FirstName = user.FirstName,
                //    ConfirmationHash = user.ConfirmationHash,
                //    CancelationHash = user.CancelationHash,
                //    SchoolName = model.SchoolName,
                //    WorkshopTitle = workshop.Title
                //}).SendEmail();
            }

            return Json(new
            {
                status = res.ValidationErrors.Count == 0,
                errors = res.ValidationErrors
            });
        }
    }
}