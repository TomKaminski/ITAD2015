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

namespace Itad2015.Service.Concrete
{
    public class WorkshopService : EntityService<WorkshopGetDto, WorkshopPostDto, Workshop>, IWorkshopService
    {
        private readonly IWorkshopRepository _repository;
        private readonly IImageProcessorService _imageProcessorService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGuestRepository _guestRepository;

        public WorkshopService(IUnitOfWork unitOfWork, IWorkshopRepository repository, IImageProcessorService imageProcessorService, IGuestRepository guestRepository) : base(unitOfWork, repository)
        {
            _repository = repository;
            _imageProcessorService = imageProcessorService;
            _guestRepository = guestRepository;
            _unitOfWork = unitOfWork;
        }

        public void Delete(int id, string path)
        {
            var obj = _repository.Find(id);
            _imageProcessorService.DeleteImagesByPath(path+obj.Title);
            _repository.Delete(obj);
            _unitOfWork.Commit();
        }

        public SingleServiceResult<IEnumerable<WorkshopGuestListGetDto>> GetWorkshopGuestsGrouped()
        {
            var workshopGuests = _guestRepository.Include(x => x.WorkshopGuest).Where(x => x.WorkshopGuestId != null).ToList();
            var workshops = _repository.GetAll().ToList();

            var workshoplist = workshops.Select(Mapper.Map<WorkshopGuestListGetDto>).ToList();
            foreach (var item in workshoplist)
            {
                var thisWorkshopGuests = workshopGuests.Where(x => x.WorkshopGuest.WorkshopId == item.Id).Select(Mapper.Map<GuestGetDto>);
                item.Guests = thisWorkshopGuests.ToList();
            }
            return new SingleServiceResult<IEnumerable<WorkshopGuestListGetDto>>(workshoplist);
        }

        
    }
}
