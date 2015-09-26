using System;
using System.Data.Entity;
using Itad2015.Model;

namespace Itad2015.Repository
{
    public class EfDatabaseFactory : IDatabaseFactory,IDisposable
    {
        private ItadDbContext _context;
        private bool _disposed;
        public DbContext Get()
        {
            return _context ?? (_context = new ItadDbContext());
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
                _context.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
