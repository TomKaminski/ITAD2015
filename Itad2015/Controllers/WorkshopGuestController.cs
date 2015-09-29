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
        public JsonResult Register(RegisterWorkshopGuestViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    status = false,
                    error = ModelState.First(x => x.Value.Errors.Count > 0).Value.Errors.First()
                });

            var mappedGuestModel = Mapper.Map<GuestPostDto>(model);
            var mappedWorkshopGuestModel = Mapper.Map<WorkshopGuestPostDto>(model);

            var guestRegisterResult = _workshopGuestService.Register(mappedGuestModel, mappedWorkshopGuestModel);

            if (!guestRegisterResult.ValidationErrors.Any())
            {
                //TODO: SEND EMAIL
            }

            return Json(new
            {
                status = guestRegisterResult.ValidationErrors.Any(),
                errors = guestRegisterResult.ValidationErrors
            });
        }
    }
}