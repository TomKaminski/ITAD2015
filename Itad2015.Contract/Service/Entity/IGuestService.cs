using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;

namespace Itad2015.Contract.Service.Entity
{
    public interface IGuestService : IEntityService<GuestGetDto, GuestPostDto>
    {
        int RegisteredGuestsCount();

        SingleServiceResult<GuestGetDto> Register(GuestPostDto model);
    }
}
