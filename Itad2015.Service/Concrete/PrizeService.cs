using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Service.Concrete
{
    public class PrizeService : EntityService<PrizeGetDto, PrizePostDto, Prize>, IPrizeService
    {
        private readonly IPrizeRepository _repository;

        public PrizeService(IUnitOfWork unitOfWork, IPrizeRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
        }
    }
}
