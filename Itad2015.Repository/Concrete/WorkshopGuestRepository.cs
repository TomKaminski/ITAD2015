using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Repository.Concrete
{
    public class WorkshopGuestRepository : GenericRepository<WorkshopGuest>, IWorkshopGuestRepository
    {
        private IUnitOfWork _unitOfWork;
        public WorkshopGuestRepository(IDatabaseFactory factory, IUnitOfWork unitOfWork) : base(factory, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
