using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Service.Concrete
{
    public class WorkshopGuestService : EntityService<WorkshopGuestGetGto, WorkshopGuestPostDto, WorkshopGuest>, IWorkshopGuestService
    {
        private readonly IWorkshopGuestRepository _repository;

        public WorkshopGuestService(IUnitOfWork unitOfWork, IWorkshopGuestRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }
    }
}
