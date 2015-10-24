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
            var workshopResult = _workshopService.GetAll();
            var workshops = workshopResult.Result.Select(Mapper.Map<WorkshopListItem>).ToList();
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

        public ActionResult Delete(int id)
        {
            return View(Mapper.Map<WorkshopListItem>(_workshopService.Get(id).Result));
        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var path = Server.MapPath("~/Content/images/workshop/");
            _workshopService.Delete(id, path);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var obj = Mapper.Map<WorkshopEditViewModel>(_workshopService.Get(id).Result);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(WorkshopEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedModel = Mapper.Map<WorkshopPostDto>(model);
                if (model.Image != null)
                {
                    var virtualPath = "~/Content/images/workshop/" + model.Title;
                    var path = Server.MapPath(virtualPath);

                    _imageProcessorService.DeleteImagesByPath(path);
                    _imageProcessorService.ProcessAndSaveImage(HttpPostedFileBaseToByteConverter.Convert(model.Image.InputStream), path);

                    mappedModel.ImgPath = virtualPath;
                }

                _workshopService.Edit(mappedModel);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult WorkshopGuestsList()
        {
            var mappedModel = _workshopService.GetWorkshopGuestsGrouped().Result.Select(Mapper.Map<WorkshopGuestViewModel>).ToList();
            return View(mappedModel);
        }


    }
}