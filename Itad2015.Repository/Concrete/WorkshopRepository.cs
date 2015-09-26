using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Repository.Concrete
{
    public class WorkshopRepository : GenericRepository<Workshop>, IWorkshopRepository
    {
        public WorkshopRepository(IDatabaseFactory factory) : base(factory)
        {
        }
    }
}
