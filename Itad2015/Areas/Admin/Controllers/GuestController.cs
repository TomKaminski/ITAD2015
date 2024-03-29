﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Areas.Admin.ViewModels.PdfViewModels;
using Itad2015.Contract.DTO.Base;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Contract.Service.Entity;
using Itad2015.Helpers.Email;
using Itad2015.Hubs;
using Itad2015.ViewModels.Email;
using Microsoft.AspNet.SignalR;

namespace Itad2015.Areas.Admin.Controllers
{
    public class GuestController : AdminBaseController
    {
        private readonly IGuestService _guestService;
        private readonly IPdfService _pdfService;
        private readonly IQrCodeGenerator _qrCodeGenerator;

        public GuestController(IGuestService guestService, IQrCodeGenerator qrCodeGenerator, IPdfService pdfService)
        {
            _guestService = guestService;
            _qrCodeGenerator = qrCodeGenerator;
            _pdfService = pdfService;
        }

        // GET: Admin/Guest
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll(bool forShirts = false)
        {
            var registeredGuests = _guestService.GetAll(x => !x.Cancelled && x.ConfirmationTime != null);

            if (forShirts)
            {
                registeredGuests.Result = registeredGuests.Result.Where(x => x.Size != Size.NoShirt);
            }

            var mappedModel = registeredGuests.Result.Select(Mapper.Map<AdminGuestViewModel>).ToList();
            return Json(mappedModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CheckIn(int id)
        {
            var result = _guestService.CheckIn(id);
            return Json(new
            {
                status = result.Result,
                errors = result.ValidationErrors ?? new List<string>()
            });
        }
        [HttpPost]
        public JsonResult CheckOut(int id)
        {
            var result = _guestService.CheckOut(id);
            return Json(new
            {
                status = result.Result,
                errors = result.ValidationErrors ?? new List<string>()
            });
        }

        public ActionResult SendQr()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SendQr(string connectionId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<QrHub>();
            await context.Groups.Add(connectionId, connectionId);

            var allGuests = _guestService.GetAll(x => !x.Cancelled && x.ConfirmationTime != null).Result.ToList();

            var data = allGuests.Where(x => !x.QrEmailSent).ToList();

            var guestsToEdit = new List<GuestPostDto>();

            foreach (var d in data)
            {
                var qrByteArray = _qrCodeGenerator.GenerateQrAsByteArray(_qrCodeGenerator.GenerateQrCode(d.Email));
                var qrStringSrc = _qrCodeGenerator.GenerateQrCodeStringSrc(qrByteArray);
                var pdfModel = Mapper.Map<QrTicketViewModel>(d);
                pdfModel.MaxNumberForShirt = _guestService.MaxGuestsForShirt;
                pdfModel.RegisterNumber = allGuests.FindIndex(x => x.Id == d.Id) + 1;
                pdfModel.QrSrc = qrStringSrc;

                var pdfFile = _pdfService.GeneratePdfFromView(RenderViewToString("Guest", "~/Areas/Admin/Views/Guest/QrTicket.cshtml", pdfModel),
                    new[] { Server.MapPath("~/Content/pdfStyles.css") }, Server.MapPath("~/Content/fonts/sinkinsansregular.ttf"));

                await new EmailHelper<GuestQrEmail>(new GuestQrEmail(d.Email, "reset@ath.bielsko.pl",
                    "Zaproszenie na konferencję ITAD 2015.")
                    {
                        LastName = d.LastName,
                        Name = d.FirstName
                    })
                .AddAttachement(pdfFile, "Bilet.pdf")
                .AddAttachement(qrByteArray, "Bilet.png")
                .SendEmailAsync();

                d.QrEmailSent = true;
                guestsToEdit.Add(Mapper.Map<GuestGetDto, GuestPostDto>(d));

                context.Clients.Group(connectionId).notifyEmailSent(d.Email);
            }
            _guestService.EditMany(guestsToEdit);
            return Json(true);
        }

        [HttpGet]
        public ActionResult DeleteSpecialByBukkakaPlz(string email)
        {
            var guest = _guestService.FirstOrDefault(x => x.Email == email);
            _guestService.Delete(guest.Result.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}