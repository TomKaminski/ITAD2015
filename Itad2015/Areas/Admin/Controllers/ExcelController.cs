using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.Helpers;
using Itad2015.Areas.Admin.ViewModels;
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
    public class ExcelController : AdminBaseController
    {
        private readonly IExcelService _excelService;
        private readonly IGuestService _guestService;
        private readonly IInvitedPersonService _invitedPersonService;

        private const string BasePath = "~/Content/static/excel/guests/";

        public ExcelController(IExcelService excelService, IGuestService guestService, IInvitedPersonService invitedPersonService)
        {
            _excelService = excelService;
            _guestService = guestService;
            _invitedPersonService = invitedPersonService;
        }

        // GET: Admin/Excel
        public ActionResult Index()
        {
            System.IO.Directory.CreateDirectory(Server.MapPath(BasePath));
            return View();
        }

        [HttpPost]
        public ActionResult UploadExcelFile(ExcelPostViewModel model)
        {
            var virtualPath = $"{BasePath}{model.File.FileName}";
            var path = Server.MapPath(virtualPath);

            _excelService.SaveFile(HttpPostedFileBaseToByteConverter.Convert(model.File.InputStream), path);

            var mappedModel = Mapper.Map<ExcelGetDataViewModel>(model);
            mappedModel.FileName = model.File.FileName;

            return View(mappedModel);
        }

        [HttpPost]
        public JsonResult GetUploadedFileData(ExcelGetDataViewModel model)
        {
            var virtualPath = $"{BasePath}{model.FileName}";
            var path = Server.MapPath(virtualPath);

            var mappedModel = Mapper.Map<ExcelPostFileDto>(model);
            mappedModel.FilePath = path;

            var emailData = _excelService.GetEmailData(mappedModel).Select(Mapper.Map<ExcelListItemViewModel>).ToList();

            var alreadyInvitedPeople = _invitedPersonService.GetAll().Result.Select(x => x.Email);

            foreach (var item in alreadyInvitedPeople.Select(email => emailData.Single(x => x.Email == email)))
            {
                item.EmailSent = true;
            }


            return Json(emailData);
        }

        [HttpPost]
        public async Task<JsonResult> SendInvites(ExcelGetDataViewModel model)
        {
            var virtualPath = $"{BasePath}{model.FileName}";
            var path = Server.MapPath(virtualPath);

            var mappedModel = Mapper.Map<ExcelPostFileDto>(model);
            mappedModel.FilePath = path;

            var context = GlobalHost.ConnectionManager.GetHubContext<ExcelHub>();
            await context.Groups.Add(model.ConnectionId, model.ConnectionId);

            var data = _excelService.GetEmailData(mappedModel).Select(Mapper.Map<ExcelListItemViewModel>).ToList();

            var alreadyInvitedPeople = _invitedPersonService.GetAll().Result.Select(x => x.Email);

            foreach (var item in alreadyInvitedPeople.Select(email => data.Single(x => x.Email == email)))
            {
                item.EmailSent = true;
            }

            var invitesToAdd = new List<InvitedPersonPostDto>();

            foreach (var d in data.Where(d => !d.EmailSent))
            {
                await new EmailHelper<GuestInviteEmail>(new GuestInviteEmail(d.Email, "reset@ath.bielsko.pl",
                    "Zaproszenie na konferencję ITAD 2015.")
                {
                    LastName = d.LastName,
                    Name = d.Name
                }).SendEmailAsync();

                invitesToAdd.Add(Mapper.Map<ExcelListItemViewModel,InvitedPersonPostDto>(d));

                context.Clients.Group(model.ConnectionId).notifyEmailSent(d.Email);
            }

            _invitedPersonService.CreateMany(invitesToAdd);

            _excelService.DeleteFile(path);
            return Json(true);
        }


        public FileResult GetNotOrderedShirts()
        {
            var notOrderedShirts = _guestService.GetAll(x => !x.ShirtOrdered && x.ConfirmationTime != null).Result.ToList();

            var file = _excelService.GetShirtsFile(notOrderedShirts.Select(Mapper.Map<GuestShirtGetDto>).ToList());

            notOrderedShirts.ForEach(x=>x.ShirtOrdered=true);

            _guestService.EditMany(notOrderedShirts.Select(Mapper.Map<GuestPostDto>).ToList());

            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"NotOrderedShirts-{DateTime.Today.ToShortDateString()}.xlsx");
        }


        public ActionResult ImportShirts()
        {
            return View();
        }

        public JsonResult GetAllShirtsData()
        {
            var registeredGuests = _guestService.GetAll(x => !x.Cancelled && x.ConfirmationTime != null);

            var mappedModel = registeredGuests.Result.Select(Mapper.Map<AdminGuestShirtViewModel>).ToList();
            return Json(mappedModel, JsonRequestBehavior.AllowGet);
        }
    }
}