using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Contract.Service.Entity;
using Itad2015.ViewModels;
using Itad2015.ViewModels.Home;

namespace Itad2015.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGuestService _guestService;
        private readonly IWorkshopService _workshopService;
        private readonly IWorkshopGuestService _workshopGuestService;


        private const int NormalTicketsCount = 350;

        public HomeController(IGuestService guestService, IWorkshopService workshopService, IWorkshopGuestService workshopGuestService)
        {
            _guestService = guestService;
            _workshopService = workshopService;
            _workshopGuestService = workshopGuestService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var workshops = _workshopService.GetAll().Result.Select(Mapper.Map<WorkshopDropdownViewModel>).ToList();

            var workshopGuestsGrouped =
                _workshopGuestService.GetAll()
                    .Result.GroupBy(x => x.WorkshopId)
                    .Select(x => new { Id = x.Key, Count = x.Count() }).ToList();

            foreach (var workshop in workshops)
            {
                var firstOrDefault = workshopGuestsGrouped.FirstOrDefault(x => x.Id == workshop.Id);
                workshop.LeftParticipants = workshop.MaxParticipants - firstOrDefault?.Count ?? workshop.MaxParticipants;
            }

            var normalTicketsLeft = NormalTicketsCount - _guestService.Count();

            return View(new IndexViewModel
            {
                IndexGuestModel = new IndexGuestModel
                {
                    NormalTicketsLeft = normalTicketsLeft
                },
                IndexWorkshopGuestModel = new IndexWorkshopGuestModel
                {
                    WorkshopDropdownList = workshops
                }
            });
        }
    }
}