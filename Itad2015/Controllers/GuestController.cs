using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.ViewModels.Guest;

namespace Itad2015.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Register(RegisterGuestViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    status = false,
                    error = ModelState.First(x => x.Value.Errors.Count > 0).Value.Errors.First()
                });

            var mappedModel = Mapper.Map<GuestPostDto>(model);

            var guestRegisterResult = _guestService.Register(mappedModel);

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