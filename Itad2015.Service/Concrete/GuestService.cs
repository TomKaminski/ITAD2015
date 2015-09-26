using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Service.Concrete
{
    public class GuestService : EntityService<GuestGetDto, GuestPostDto,Guest>,IGuestService
    {
        private readonly IGuestRepository _repository;
        
        public GuestService(IUnitOfWork unitOfWork, IGuestRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }
    }
}
