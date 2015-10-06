﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.ViewModels.Base;
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
                //new EmailHelper<GuestRegisterEmail>(new GuestRegisterEmail(res.Result.Email,"itaddbb@gmail.com","Rejestracja na konferencję.")
                //{
                //    LastName = res.Result.LastName,
                //    FirstName = res.Result.FirstName,
                //    ConfirmationHash = res.Result.ConfirmationHash,
                //    CancelationHash = res.Result.CancelationHash,
                //}).SendEmail();
            }

            return Json(new
            {
                status = res.ValidationErrors.Count == 0,
                errors = res.ValidationErrors
            });
        }

        [HttpGet]
        public ActionResult ConfirmRegistration(int id, string confirmationCode)
        {
            var result = _guestService.ConfirmRegistration(id, confirmationCode);
            //TODO: Chcemy jakiś popup na głównej, czy osobny widok z info?
            return View(Mapper.Map<BaseReturnViewModel<bool>>(result));
        }

        [HttpGet]
        public ActionResult CancelRegistration(int id, string cancelationCode)
        {
            var result = _guestService.CancelRegistration(id, cancelationCode);
            //TODO: Chcemy jakiś popup na głównej, czy osobny widok z info?
            return View(Mapper.Map<BaseReturnViewModel<bool>>(result));
        }
    }
}