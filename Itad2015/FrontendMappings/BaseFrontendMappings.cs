using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Models;
using Itad2015.Modules.Infrastructure;
using Itad2015.ViewModels.Guest;

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
        }
    }
}