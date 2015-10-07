using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.Helpers.Email;
using Itad2015.ViewModels.Base;
using Itad2015.ViewModels.Email;
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
        public JsonResult Register([Bind(Prefix = "RegisterGuestViewModel")]RegisterGuestViewModel model)
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

            var mappedModel = Mapper.Map<GuestPostDto>(model);

            var res = _guestService.Register(mappedModel);

            if (!res.ValidationErrors.Any())
            {
                new EmailHelper<GuestRegisterEmail>(new GuestRegisterEmail(res.Result.Email, "itadbb2015@gmail.com", "Rejestracja na konferencję.")
                {
                    LastName = res.Result.LastName,
                    FirstName = res.Result.FirstName,
                    ConfirmationHash = res.Result.ConfirmationHash,
                    CancelationHash = res.Result.CancelationHash,
                    Id = res.Result.Id
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