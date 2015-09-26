using System.Data.Entity;

namespace Itad2015.Repository
{
    public interface IDatabaseFactory
    {
        DbContext Get();
    }
}
