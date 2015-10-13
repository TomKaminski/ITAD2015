using System.Web.Http;
using AutoMapper;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.ViewModels.Device;

namespace Itad2015.Controllers
{
    public class DeviceController : ApiController
    {
        private readonly IUserService _userService;

        public DeviceController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public void RegisterDevice(RegisterDeviceApiModel model)
        {
            var mappedModel = Mapper.Map<ConnectedDevicePostDto>(model);
            mappedModel.UserId = _userService.GetByEmail(model.UserSignalRId).Result.Id;
        }
    }
}
