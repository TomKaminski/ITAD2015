using System.Linq;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Repository.Concrete
{
    public class WorkshopRepository : GenericRepository<Workshop>, IWorkshopRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGuestRepository _guestRepository;
        private readonly IWorkshopGuestRepository _workshopGuestRepository;
        public WorkshopRepository(IDatabaseFactory factory, IUnitOfWork unitOfWork, IGuestRepository guestRepository, IWorkshopGuestRepository workshopGuestRepository) : base(factory,unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _guestRepository = guestRepository;
            _workshopGuestRepository = workshopGuestRepository;
        }

        public override void Delete(Workshop entity)
        {
            var guestOfWorkshop = _workshopGuestRepository.GetAll(x => x.WorkshopId == entity.Id).Select(x=>x.GuestId).ToList();
            var guestsToDelete = _guestRepository.GetAll(x => guestOfWorkshop.Contains(x.Id)).ToList();
            foreach (var i in guestsToDelete)
            {
                _guestRepository.Delete(i);
            }
            _unitOfWork.Commit();
            base.Delete(entity);
        }
    }
}
