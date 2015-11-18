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
            var errors = ValidateRegister(workshop,guestModel);
            if (!errors.Any())
            {
                guestModel.RegistrationTime = DateTime.Now;


                var guestEntity = Mapper.Map<Guest>(guestModel);
                var workshopGuestEntity = Mapper.Map<WorkshopGuest>(workshopGuestModel);

                guestEntity.CancelationHash = _hashGenerator.CreateHash(guestEntity.Email + "cancel");
                guestEntity.ConfirmationHash = _hashGenerator.CreateHash(guestEntity.Email + "confirm");

                var guestObj = _guestRepository.Add(guestEntity);

                workshopGuestEntity.GuestId = guestObj.Id;
                var workshopObj = _repository.Add(workshopGuestEntity);
                guestObj.WorkshopGuestId = workshopObj.Id;
                _guestRepository.Edit(guestObj);

                _unitOfWork.Commit();

                return new SingleServiceResult<GuestGetDto, WorkshopGetDto>(Mapper.Map<GuestGetDto>(guestObj),workshop,new List<string>());
            }
            return new SingleServiceResult<GuestGetDto, WorkshopGetDto>(new GuestGetDto(),new WorkshopGetDto(), errors);
        }

        private List<string> ValidateRegister(WorkshopGetDto workshop, GuestPostDto guestModel)
        {
            var errors = new List<string>();
            if (_repository.Count(x=>x.WorkshopId == workshop.Id) >= workshop.MaxParticipants)
                errors.Add("Przepraszamy, zabrakło miejsc na wybrany przez Ciebie warsztat");

            if (_guestRepository.FirstOrDefault(x => x.Email == guestModel.Email && !x.Cancelled) != null)
                errors.Add("Ten email jest już zarejestrowany!");
            return errors;
        }

        public SingleServiceResult<WorkshopGuestExtendedGetDto> GetExtendedWorkshopGuest(int id)
        {
            var extendedGuest = Mapper.Map<WorkshopGuestExtendedGetDto>(_repository.Include(x => x.Guest).Single(x => x.Id == id));
            return new SingleServiceResult<WorkshopGuestExtendedGetDto>(extendedGuest);
        }

        public MultipleServiceResult<WorkshopGuestExtendedGetDto> GetExtendedWorkshopGuests()
        {
            var extendedGuests = _repository.Include(x => x.Guest).Select(Mapper.Map<WorkshopGuestExtendedGetDto>);
            return new MultipleServiceResult<WorkshopGuestExtendedGetDto>(extendedGuests);
        }

        public override void Delete(int id)
        {
            var guest = _guestRepository.FirstOrDefault(x => x.WorkshopGuestId == id);
            guest.WorkshopGuestId = null;
            _guestRepository.Edit(guest);
            base.Delete(id);
        }
    }
}
