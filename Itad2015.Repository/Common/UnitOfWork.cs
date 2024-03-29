﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Itad2015.Repository.Common
{
    /// <summary>
    /// The Entity Framework implementation of IUnitOfWork
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        private DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        public UnitOfWork(IDatabaseFactory factory)
        {
            _dbContext = factory.Get();
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit()
        {
            // Save changes with the default options
            _dbContext.Configuration.ValidateOnSaveEnabled = false;
            var savedEntities = _dbContext.SaveChanges();
            _dbContext.Configuration.ValidateOnSaveEnabled = true;
            return savedEntities;
        }

        public async Task<int> CommitAsync()
        {
            _dbContext.Configuration.ValidateOnSaveEnabled = false;
            var savedEntities = await _dbContext.SaveChangesAsync();
            _dbContext.Configuration.ValidateOnSaveEnabled = true;
            return savedEntities;
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_dbContext == null) return;
            _dbContext.Dispose();
            _dbContext = null;
        }
    }
}
