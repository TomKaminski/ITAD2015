using Autofac;
using AutoMapper;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Model.Concrete;
using Itad2015.Modules.Infrastructure;

namespace Itad2015.Modules.Modules
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            InitMappings();
        }

        private static void InitMappings()
        {
            Mapper.CreateMap<GuestPostDto, Guest>().IgnoreNotExistingProperties();
            Mapper.CreateMap<Guest, GuestGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<WorkshopGuestPostDto, WorkshopGuest>().IgnoreNotExistingProperties();
            Mapper.CreateMap<WorkshopGuest, WorkshopGuestGetGto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<WorkshopPostDto, Workshop>().IgnoreNotExistingProperties();
            Mapper.CreateMap<Workshop, WorkshopGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<PrizePostDto, Prize>().IgnoreNotExistingProperties();
            Mapper.CreateMap<Prize, PrizeGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<UserPostDto, User>().IgnoreNotExistingProperties();
            Mapper.CreateMap<User, UserGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<InvitedPersonPostDto, InvitedPerson>().IgnoreNotExistingProperties();
            Mapper.CreateMap<InvitedPerson, InvitedPersonGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<Workshop, WorkshopGuestListGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<WorkshopGuest, WorkshopGuestExtendedGetDto>().IgnoreNotExistingProperties();

            Mapper.CreateMap<Guest, GuestExtendedWorkshopGetDto>()
                .ForMember(x => x.SchoolName, opt => opt.MapFrom(m => m.WorkshopGuest.SchoolName));
        }
    }
}
