using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.Api;
using Itad2015.Contract.DTO.Base;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;
using Itad2015.Service.Helpers.Interfaces;

namespace Itad2015.Service.Concrete
{
    public class GuestService : EntityService<GuestGetDto, GuestPostDto, Guest>, IGuestService
    {
        private readonly IGuestRepository _repository;
        private readonly IHashGenerator _hashGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkshopGuestRepository _workshopGuestRepository;

        public int MaxNormalRegisteredGuests => 350;
        public int MaxGuestsForShirt => 300;
        public SingleServiceResult<bool> RegisterAdmin(GuestAdminPostDto model)
        {
            if (model.WorkshopId != null)
            {
                var guestMappedModel = Mapper.Map<Guest>(model);
                var mappedWorkshopGuestModel = Mapper.Map<WorkshopGuestPostDto>(model);
                var workshopMappedModel = Mapper.Map<WorkshopGuest>(mappedWorkshopGuestModel);

                var guestObj = _repository.Add(guestMappedModel);

                workshopMappedModel.GuestId = guestObj.Id;
                var workshopObj = _workshopGuestRepository.Add(workshopMappedModel);
                guestObj.WorkshopGuestId = workshopObj.Id;
                _repository.Edit(guestObj);               
            }
            else
            {
                _repository.Add(Mapper.Map<Guest>(model));
            }
            _unitOfWork.Commit();
            return new SingleServiceResult<bool>(true);
        }

        public GuestService(IUnitOfWork unitOfWork, IGuestRepository repository, IHashGenerator hashGenerator, IWorkshopGuestRepository workshopGuestRepository) : base(unitOfWork, repository)
        {
            _repository = repository;
            _hashGenerator = hashGenerator;
            _workshopGuestRepository = workshopGuestRepository;
            _unitOfWork = unitOfWork;
        }

        public SingleServiceResult<GuestGetDto> Register(GuestPostDto model)
        {
            var registeredPersons = _repository.GetAll().ToList();
            var errors = ValidateRegister(registeredPersons, model.Email);
            if (!errors.Any())
            {
                var possibleCanceledGuest = registeredPersons.FirstOrDefault(x => x.Email == model.Email);
                if (possibleCanceledGuest != null)
                {
                    _repository.Delete(possibleCanceledGuest);
                }

                model.RegistrationTime = DateTime.Now;
                var entity = Mapper.Map<Guest>(model);

                entity.CancelationHash = _hashGenerator.CreateHash(entity.Email + "cancel");
                entity.ConfirmationHash = _hashGenerator.CreateHash(entity.Email + "confirm");

                var obj = Mapper.Map<GuestGetDto>(_repository.Add(entity));
                _unitOfWork.Commit();

                return new SingleServiceResult<GuestGetDto>(obj, new List<string>());
            }
            return new SingleServiceResult<GuestGetDto>(new GuestGetDto(), errors);
        }

        public SingleServiceResult<bool, int> ConfirmRegistration(int id, string confirmHash)
        {
            var guests = _repository.GetAll();
            var guest = guests.FirstOrDefault(x => x.Id == id);
            var errors = ValidateConfirm(guest, confirmHash);
            if (!errors.Any() && guest != null)
            {
                var confirmedGuests = guests.Where(x => x.ConfirmationTime != null && !x.Cancelled).ToList();
                if (confirmedGuests.Count >= MaxGuestsForShirt)
                {
                    guest.Size = Model.Enums.Size.NoShirt;
                }
                guest.ConfirmationTime = DateTime.Now;
                _repository.Edit(guest);
                _unitOfWork.Commit();
                return new SingleServiceResult<bool, int>(true, confirmedGuests.Count + 1, errors);
            }
            return new SingleServiceResult<bool, int>(false, 0, errors);
        }

        public SingleServiceResult<bool> CancelRegistration(int id, string cancelHash)
        {
            var guest = _repository.Find(id);
            var errors = ValidateCancel(guest, cancelHash);
            if (!errors.Any())
            {
                _repository.Delete(guest);
                _unitOfWork.Commit();
                return new SingleServiceResult<bool>(true, errors);
            }
            return new SingleServiceResult<bool>(false, errors);
        }

        public SingleServiceResult<bool> CheckIn(int id)
        {
            var guest = _repository.Find(id);
            if (guest != null)
            {
                guest.CheckInDate = DateTime.Now;
                _repository.Edit(guest);
                _unitOfWork.Commit();
                return new SingleServiceResult<bool>(true);
            }
            return new SingleServiceResult<bool>(false, new List<string> { "Nie ma takiego użytkownika." });
        }

        public SingleServiceResult<GuestApiDto> CheckIn(string email)
        {
            var guest = _repository.FirstOrDefault(x => x.Email == email);

            if (guest == null)
            {
                return new SingleServiceResult<GuestApiDto>(new GuestApiDto
                {
                    Date = DateTime.Now,
                    Status = false,
                    Error = $"Email: {email} nie występuje na liście uczestników"
                });
            }

            if (guest.CheckInDate != null)
            {
                return new SingleServiceResult<GuestApiDto>(new GuestApiDto
                {
                    Date = DateTime.Now,
                    Status = false,
                    Error = $"{guest.FirstName} {guest.LastName} został już zarejestrowany!"
                });
            }

            guest.CheckInDate = DateTime.Now;
            _repository.Edit(guest);
            _unitOfWork.Commit();
            return new SingleServiceResult<GuestApiDto>(new GuestApiDto
            {
                Date = DateTime.Now,
                Status = true,
                Person = new GuestApiDtoPerson
                {
                    CheckInDate = guest.CheckInDate,
                    Email = guest.Email,
                    FirstName = guest.FirstName,
                    Id = guest.Id,
                    LastName = guest.LastName,
                    Size = (Size)Enum.Parse(typeof(Size), guest.Size.ToString())
                }
            });
        }

        public SingleServiceResult<bool> CheckOut(int id)
        {
            var guest = _repository.Find(id);
            if (guest != null)
            {
                guest.CheckInDate = null;
                _repository.Edit(guest);
                _unitOfWork.Commit();
                return new SingleServiceResult<bool>(true);
            }
            return new SingleServiceResult<bool>(false, new List<string> { "Nie ma takiego użytkownika." });
        }



        private List<string> ValidateConfirm(Guest guest, string confirmationHash)
        {
            var errors = new List<string>();
            if (guest == null)
            {
                errors.Add("Uczestnik nie istnieje!");
                return errors;
            }
            if (guest.ConfirmationTime != null)
                errors.Add("Uczestnik został już potwierdzony!");
            if (guest.ConfirmationHash != confirmationHash)
                errors.Add("Błędny kod.");
            return errors;
        }

        private List<string> ValidateCancel(Guest guest, string cancelationHash)
        {
            var errors = new List<string>();
            if (guest == null)
            {
                errors.Add("Uczestnik został już wypisany!");
                return errors;
            }
            if (guest.CancelationHash != cancelationHash)
                errors.Add("Błędny kod.");
            return errors;
        }

        private List<string> ValidateRegister(List<Guest> guests, string email)
        {
            var errors = new List<string>();

            if (guests.FirstOrDefault(x => x.Email == email && !x.Cancelled) != null)
                errors.Add("Ten email jest już zarejestrowany!");

            if (guests.Count(x => !x.Cancelled && x.WorkshopGuestId == null) >= MaxNormalRegisteredGuests)
                errors.Add("Przepraszamy, brak miejsc.");

            return errors;
        }
    }
}
