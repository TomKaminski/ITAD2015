using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.Helpers;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Helpers.Email;
using Itad2015.Hubs;
using Itad2015.ViewModels.Email;
using Microsoft.AspNet.SignalR;

namespace Itad2015.Areas.Admin.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IExcelService _excelService;

        private const string BasePath = "~/Content/static/excel/guests/";

        public ExcelController(IExcelService excelService)
        {

            _excelService = excelService;
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

            return Json(_excelService.GetEmailData(mappedModel).Select(Mapper.Map<ExcelListItemViewModel>).ToList());
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

            foreach (var d in data)
            {
                await new EmailHelper<GuestInviteEmail>(new GuestInviteEmail(d.Email, "reset@ath.bielsko.pl",
                    "Zaproszenie na konferencję ITAD 2015.")
                {
                    LastName = d.LastName,
                    Name = d.Name
                }).SendEmailAsync();

                context.Clients.Group(model.ConnectionId).notifyEmailSent(d.Email);
            }

            _excelService.DeleteFile(path);
            return Json(true);
        }
    }
}