using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.Helpers;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Contract.Service.Entity;

namespace Itad2015.Areas.Admin.Controllers
{
    public class WorkshopController : AdminBaseController
    {
        private readonly IImageProcessorService _imageProcessorService;
        private readonly IWorkshopService _workshopService;
        public WorkshopController(IImageProcessorService imageProcessorService, IWorkshopService workshopService)
        {
            _imageProcessorService = imageProcessorService;
            _workshopService = workshopService;
        }

        // GET: Admin/Workshop
        public ActionResult Index()
        {
            var workshops = _workshopService.GetAll().Select(Mapper.Map<WorkshopListItem>).ToList();
            return View(workshops);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WorkshopCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var virtualPath = "~/Content/images/workshop/" + model.Title;
                var path = Server.MapPath(virtualPath);

                _imageProcessorService.ProcessAndSaveImage(HttpPostedFileBaseToByteConverter.Convert(model.Image.InputStream), path);

                var mappedModel = Mapper.Map<WorkshopPostDto>(model);
                mappedModel.ImgPath = virtualPath;

                _workshopService.Create(mappedModel);

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}