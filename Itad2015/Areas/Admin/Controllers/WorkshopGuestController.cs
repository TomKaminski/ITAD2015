using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.Service.Entity;

namespace Itad2015.Areas.Admin.Controllers
{
    public class WorkshopGuestController : AdminBaseController
    {
        private readonly IWorkshopGuestService _workshopGuestService;

        public WorkshopGuestController(IWorkshopGuestService workshopGuestService)
        {
            _workshopGuestService = workshopGuestService;
        }

        [HttpGet]
        public ActionResult DeleteGuest(int id)
        {
            return View(Mapper.Map<WorkshopGuestExtendedViewModel>(_workshopGuestService.GetExtendedWorkshopGuest(id).Result));
        }

        [HttpPost]
        [ActionName("DeleteGuest")]
        public ActionResult DeleteGuestPost(int id)
        {
            _workshopGuestService.Delete(id);
            return RedirectToAction("WorkshopGuestsList", "Workshop");
        }
    }
}