using Autofac;
using AutoMapper;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Model.Concrete;

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

            Mapper.CreateMap<GuestPostDto, Guest>();
            Mapper.CreateMap<Guest, GuestGetDto>();

            Mapper.CreateMap<WorkshopGuestPostDto, WorkshopGuest>();
            Mapper.CreateMap<WorkshopGuest, WorkshopGuestGetGto>();

            Mapper.CreateMap<WorkshopPostDto, Workshop>();
            Mapper.CreateMap<Workshop, WorkshopGetDto>();

            Mapper.CreateMap<PrizePostDto, Prize>();
            Mapper.CreateMap<Prize, PrizeGetDto>();

        }
    }
}
