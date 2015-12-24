using System;
using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Areas.Admin.ViewModels.PdfViewModels;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.Base;
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
            Mapper.CreateMap<UserGetDto, UserListViewModel>().IgnoreNotExistingProperties();
            Mapper.CreateMap<UserGetDto, UserEditViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap<UserCreateViewModel, UserPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<UserEditViewModel, UserPostDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<WorkshopCreateViewModel, WorkshopPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<WorkshopGetDto, WorkshopListItem>().IgnoreNotExistingProperties();
            Mapper.CreateMap<WorkshopGetDto, WorkshopEditViewModel>().IgnoreNotExistingProperties();
            Mapper.CreateMap<WorkshopEditViewModel, WorkshopPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<RegisterGuestViewModel, GuestPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<GuestGetDto, GuestPostDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<RegisterWorkshopGuestViewModel, GuestPostDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<RegisterWorkshopGuestViewModel, WorkshopGuestPostDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<WorkshopGetDto, WorkshopDropdownViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap<AdminCreateGuestViewModel, GuestAdminPostDto>()
                .ForMember(x => x.PolicyAccepted, s => s.UseValue(true))
                .ForMember(x => x.RegistrationTime, s => s.UseValue(DateTime.Now))
                .ForMember(x => x.ConfirmationTime, s => s.UseValue(DateTime.Now.AddSeconds(1)))
                .ForMember(x => x.ShirtOrdered, s => s.UseValue(true))
                .ForMember(x => x.Info, s => s.UseValue("Registered by admin tool."))
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<GuestAdminPostDto,WorkshopGuestPostDto>()
                .IgnoreNotExistingProperties();

            Mapper.CreateMap<GuestGetDto, AdminGuestViewModel>()
                .ForMember(x => x.Size, opt => opt.MapFrom(m => m.Size.ToString()))
                .AfterMap((src, dest) =>
                   {
                       dest.IsCheckIn = src.CheckInDate != null;
                   }).IgnoreNotExistingProperties();

            Mapper.CreateMap<ExcelPostViewModel, ExcelPostFileDto>().IgnoreNotExistingProperties();
            Mapper.CreateMap<ExcelPostViewModel, ExcelGetDataViewModel>().IgnoreNotExistingProperties();
            Mapper.CreateMap<ExcelGetDataViewModel, ExcelPostFileDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<ExcelGetDataViewModel, ExcelPostViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap<ExcelFileItem, ExcelListItemViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap<RegisterDeviceApiModel, ConnectedDevicePostDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<GuestGetDto, GuestShirtGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<ExcelListItemViewModel, InvitedPersonPostDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<GuestGetDto, QrTicketViewModel>().IgnoreNotExistingProperties();

            Mapper.CreateMap<WorkshopGuestListGetDto, WorkshopGuestViewModel>().ForMember(x => x.Guests, opt => opt.MapFrom(k => k.Guests)).IgnoreNotExistingProperties();

            Mapper.CreateMap<WorkshopGuestExtendedGetDto, WorkshopGuestExtendedViewModel>().ForMember(x => x.Guest, opt => opt.MapFrom(k => k.Guest)).IgnoreNotExistingProperties();

            Mapper.CreateMap<GuestExtendedWorkshopGetDto, WorkshopGuestExtendedItemViewModel>().IgnoreNotExistingProperties();
        }
    }
}