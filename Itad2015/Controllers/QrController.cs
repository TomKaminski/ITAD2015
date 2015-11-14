using System.Linq;
using System.Web.Http;
using Itad2015.Contract.Service.Entity;
using Itad2015.ViewModels.Qr;

namespace Itad2015.Controllers
{
    public class QrController : ApiController
    {
        private readonly IGuestService _guestService;
        public QrController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        private const string Token = "kn432k4n32b4325n34lk5ms23423423423901adsjkdn5465ujojzcxzasdasdas";

        [System.Web.Http.HttpPost]
        public object GetGuestsCount()
        {
            var guests = _guestService.GetAll(x => x.ConfirmationTime != null).Result.ToList();

            return new
            {
                maxPerson = guests.Count,
                registeredPerson = guests.Count(x => x.CheckInDate != null)
            };
        }

        [System.Web.Http.HttpPost]
        public object CheckInApp(QrCheckInViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.Token == Token)
                {
                    var result = _guestService.CheckIn(model.Email);
                    return Json(result.Result);
                }
                return Json(new
                {
                    Status = false,
                    Error = "Urządzenie nie posiada prawidłowego kodu autoryzacji"
                });
            }
            return new
            {
                Status = false,
                Error = ModelState.Select(x => x.Value.Errors).First(y => y.Count > 0).Select(x => x.ErrorMessage).First()
            };
        }
    }
}
