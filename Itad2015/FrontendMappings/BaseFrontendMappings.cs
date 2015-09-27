using AutoMapper;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Models;
using Itad2015.Modules.Infrastructure;

namespace Itad2015.FrontendMappings
{
    public static class BaseFrontendMappings
    {
        public static void Initialize()
        {
            Mapper.CreateMap<UserGetDto, AppUserState>().IgnoreNotExistingProperties();
        }
    }
}