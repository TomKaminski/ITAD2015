using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Repository.Concrete
{
    public class InvitedPersonRepository : GenericRepository<InvitedPerson>, IInvitedPersonRepository
    {
        private IUnitOfWork _unitOfWork;
        public InvitedPersonRepository(IDatabaseFactory factory, IUnitOfWork unitOfWork) : base(factory, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
