using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Models;
using Itad2015.Modules.Infrastructure;
using Itad2015.ViewModels;
using Itad2015.ViewModels.Base;
using Itad2015.ViewModels.Device;
using Itad2015.ViewModels.Guest;
using Itad2015.ViewModels.Home;

namespace Itad2015.FrontendMappings
{
    public static class BaseFrontendMappings
    {
        public static void Initialize()
        {
            Mapper.CreateMap<UserGetDto, AppUserState>().IgnoreNotExistingProperties();
            Mapper.CreateMap<WorkshopCreateViewModel, WorkshopPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<WorkshopGetDto, WorkshopListItem>().IgnoreNotExistingProperties();
            Mapper.CreateMap<WorkshopGetDto, WorkshopEditViewModel>().IgnoreNotExistingProperties();
            Mapper.CreateMap<WorkshopEditViewModel, WorkshopPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<RegisterGuestViewModel, GuestPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<GuestGetDto, GuestPostDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<RegisterWorkshopGuestViewModel, GuestPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<RegisterWorkshopGuestViewModel, WorkshopGuestPostDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<WorkshopGetDto, WorkshopDropdownViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap<GuestGetDto, AdminGuestViewModel>()
                .ForMember(x => x.Size, opt => opt.MapFrom(m => m.Size.ToString()))
                .AfterMap((src, dest) =>
                   {
                       dest.IsCheckIn = src.CheckInDate != null;
                   });

            Mapper.CreateMap<GuestGetDto, AdminGuestShirtViewModel>()
                .ForMember(x => x.Size, opt => opt.MapFrom(m => m.Size.ToString()));

            Mapper.CreateMap<ExcelPostViewModel, ExcelPostFileDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<ExcelPostViewModel, ExcelGetDataViewModel>().IgnoreNotExistingProperties();
            Mapper.CreateMap<ExcelGetDataViewModel, ExcelPostFileDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<ExcelGetDataViewModel, ExcelPostViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap<ExcelFileItem, ExcelListItemViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap(typeof(SingleServiceResult<>), typeof(BaseReturnViewModel<>));

            Mapper.CreateMap<RegisterDeviceApiModel, ConnectedDevicePostDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<GuestGetDto,GuestShirtGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<ExcelListItemViewModel, InvitedPersonPostDto>().IgnoreNotExistingProperties();
        }
    }
}