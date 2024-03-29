﻿using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;

namespace Itad2015.Contract.Service.Entity
{
    public interface IWorkshopGuestService : IEntityService<WorkshopGuestGetGto, WorkshopGuestPostDto>
    {
        SingleServiceResult<GuestGetDto,WorkshopGetDto> Register(GuestPostDto guestModel, WorkshopGuestPostDto workshopGuestModel);

        SingleServiceResult<WorkshopGuestExtendedGetDto> GetExtendedWorkshopGuest(int id);
    }
}
