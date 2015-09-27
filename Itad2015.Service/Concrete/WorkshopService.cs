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

        public WorkshopService(IUnitOfWork unitOfWork, IWorkshopRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }
    }
}
