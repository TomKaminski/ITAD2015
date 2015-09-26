using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Repository.Concrete
{
    public class GuestRepository : GenericRepository<Guest>, IGuestRepository
    {
        public GuestRepository(IDatabaseFactory factory) : base(factory)
        {
        }
    }
}
