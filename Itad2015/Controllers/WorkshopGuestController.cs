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
        [ValidateAntiForgeryToken]
        public JsonResult Register([Bind(Prefix = "RegisterModel")]RegisterWorkshopGuestViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    status = false,
                    error = ModelState.First(x => x.Value.Errors.Count > 0).Value.Errors.First()
                });

            var mappedGuestModel = Mapper.Map<GuestPostDto>(model);
            var mappedWorkshopGuestModel = Mapper.Map<WorkshopGuestPostDto>(model);

            var res = _workshopGuestService.Register(mappedGuestModel, mappedWorkshopGuestModel);

            if (!res.ValidationErrors.Any())
            {
                var user = res.FirstResult;
                var workshop = res.SecondResult;
                new EmailHelper<WorkshopGuestRegisterEmail>(new WorkshopGuestRegisterEmail(user.Email, "itaddbb@gmail.com", "Rejestracja na konferencję.")
                {
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    ConfirmationHash = user.ConfirmationHash,
                    CancelationHash = user.CancelationHash,
                    SchoolName = model.SchoolName,
                    WorkshopTitle = workshop.Title
                }).SendEmail();
            }

            return Json(new
            {
                status = res.ValidationErrors.Any(),
                errors = res.ValidationErrors
            });
        }
    }
}