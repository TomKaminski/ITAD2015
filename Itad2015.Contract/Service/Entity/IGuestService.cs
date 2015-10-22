using System;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.Api;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;

namespace Itad2015.Contract.Service.Entity
{
    public interface IGuestService : IEntityService<GuestGetDto, GuestPostDto>
    {

        SingleServiceResult<GuestGetDto> Register(GuestPostDto model);

        SingleServiceResult<bool, int> ConfirmRegistration(int id, string confirmHash);

        SingleServiceResult<bool> CancelRegistration(int id, string cancelHash);

        SingleServiceResult<bool> CheckIn(int id);
        SingleServiceResult<GuestApiDto> CheckIn(string email);

        SingleServiceResult<bool> CheckOut(int id);

        int MaxNormalRegisteredGuests { get;}
        int MaxGuestsForShirt { get; }
    }
}
