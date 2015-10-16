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

        const int MaxRegisteredGuests = 350;
        public GuestService(IUnitOfWork unitOfWork, IGuestRepository repository, IHashGenerator hashGenerator) : base(unitOfWork, repository)
        {
            _repository = repository;
            _hashGenerator = hashGenerator;
            _unitOfWork = unitOfWork;
        }

        public int RegisteredGuestsCount()
        {
            return _repository.Count(x => x.Cancelled != true);
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

        public SingleServiceResult<bool> ConfirmRegistration(int id, string confirmHash)
        {
            var guest = _repository.Find(id);
            var errors = ValidateConfirm(guest, confirmHash);
            if (!errors.Any())
            {
                guest.ConfirmationTime = DateTime.Now;
                _repository.Edit(guest);
                _unitOfWork.Commit();
                return new SingleServiceResult<bool>(true, errors);
            }
            return new SingleServiceResult<bool>(false, errors);
        }

        public SingleServiceResult<bool> CancelRegistration(int id, string cancelHash)
        {
            var guest = _repository.Find(id);
            var errors = ValidateCancel(guest, cancelHash);
            if (!errors.Any())
            {
                //guest.Cancelled = true;

                _repository.Delete(guest);

                //_repository.Edit(guest);
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
                    Status = false,
                    Error = $"Email: {email} nie występuje na liście uczestników"
                });
            }

            if (guest.CheckInDate != null)
            {
                return new SingleServiceResult<GuestApiDto>(new GuestApiDto
                {
                    Status = false,
                    Error = $"{guest.FirstName} {guest.LastName} został już zarejestrowany!"
                });
            }

            guest.CheckInDate = DateTime.Now;
            _repository.Edit(guest);
            _unitOfWork.Commit();
            return new SingleServiceResult<GuestApiDto>(new GuestApiDto
            {
                Status = true,
                Person = new GuestApiDtoPerson
                {
                    CheckInDate = guest.CheckInDate,
                    Email = guest.Email,
                    FirstName = guest.FirstName,
                    Id = guest.Id,
                    LastName = guest.LastName,
                    Size = (Size)Enum.Parse(typeof(Size),guest.Size.ToString())
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
            return new SingleServiceResult<bool>(false, new List<string> {"Nie ma takiego użytkownika."});
        }

        private static List<string> ValidateConfirm(Guest guest, string confirmationHash)
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
                errors.Add("Błędny kod.!");
            return errors;
        }

        private static List<string> ValidateCancel(Guest guest, string cancelationHash)
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

        private static List<string> ValidateRegister(List<Guest> guests, string email)
        {
            var errors = new List<string>();

            if (guests.FirstOrDefault(x => x.Email == email && !x.Cancelled) != null)
                errors.Add("Ten email jest już zarejestrowany!");

            if (guests.Count(x => !x.Cancelled) >= MaxRegisteredGuests)
                errors.Add("Przepraszamy, brak miejsc.");

            return errors;
        }
    }
}
