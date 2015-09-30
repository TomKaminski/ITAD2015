using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Contract.Service.Entity;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;
using Itad2015.Service.Helpers.Interfaces;

namespace Itad2015.Service.Concrete
{
    public class WorkshopGuestService : EntityService<WorkshopGuestGetGto, WorkshopGuestPostDto, WorkshopGuest>, IWorkshopGuestService
    {
        private readonly IWorkshopGuestRepository _repository;
        private readonly IWorkshopRepository _workshopRepository;
        private readonly IHashGenerator _hashGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGuestRepository _guestRepository;

        public WorkshopGuestService(IUnitOfWork unitOfWork, IWorkshopGuestRepository repository, IWorkshopRepository workshopRepository, IHashGenerator hashGenerator, IGuestRepository guestRepository) : base(unitOfWork, repository)
        {
            _repository = repository;
            _workshopRepository = workshopRepository;
            _hashGenerator = hashGenerator;
            _guestRepository = guestRepository;
            _unitOfWork = unitOfWork;
        }

        public SingleServiceResult<GuestGetDto,WorkshopGetDto> Register(GuestPostDto guestModel, WorkshopGuestPostDto workshopGuestModel)
        {

            var workshop = Mapper.Map<WorkshopGetDto>(_workshopRepository.Find(workshopGuestModel.WorkshopId));
            var errors = ValidateRegister(workshop);
            if (!errors.Any())
            {
                guestModel.RegistrationTime = DateTime.Now;


                var guestEntity = Mapper.Map<Guest>(guestModel);
                var workshopGuestEntity = Mapper.Map<WorkshopGuest>(workshopGuestModel);

                guestEntity.CancelationHash = _hashGenerator.CreateHash(guestEntity.Email + "cancel");
                guestEntity.ConfirmationHash = _hashGenerator.CreateHash(guestEntity.Email + "confirm");

                var guestObj = Mapper.Map<GuestGetDto>(_guestRepository.Add(guestEntity));

                _repository.Add(workshopGuestEntity);

                _unitOfWork.Commit();

                return new SingleServiceResult<GuestGetDto, WorkshopGetDto>(guestObj,workshop);
            }
            return new SingleServiceResult<GuestGetDto, WorkshopGetDto>(new GuestGetDto(),new WorkshopGetDto(), errors);
        }

        private List<string> ValidateRegister(WorkshopGetDto workshop)
        {
            var errors = new List<string>();
            if (_repository.Count(x=>x.WorkshopId == workshop.Id) >= workshop.MaxParticipants)
                errors.Add("Przepraszamy, brak miejsc.");
            return errors;
        }
    }
}
