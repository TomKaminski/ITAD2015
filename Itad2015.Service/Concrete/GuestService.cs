using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Itad2015.Contract.Common;
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

        private static List<string> ValidateConfirm(Guest guest, string confirmationHash)
        {
            var errors = new List<string>();
            if (guest.ConfirmationTime != null)
                errors.Add("Uczestnik został już potwierdzony!");
            if (guest.ConfirmationHash != confirmationHash)
                errors.Add("Błędny kod.!");
            return errors;
        }

        private static List<string> ValidateCancel(Guest guest, string confirmationHash)
        {
            var errors = new List<string>();
            if (guest.Cancelled)
                errors.Add("Uczestnik został już wypisany!");
            if (guest.ConfirmationHash != confirmationHash)
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
