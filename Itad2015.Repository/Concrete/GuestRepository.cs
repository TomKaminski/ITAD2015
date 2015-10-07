using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Repository.Concrete
{
    public class GuestRepository : GenericRepository<Guest>, IGuestRepository
    {
        private IUnitOfWork _unitOfWork;
        public GuestRepository(IDatabaseFactory factory, IUnitOfWork unitOfWork) : base(factory, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
