using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itad2015.Model.Common;

namespace Itad2015.Repository.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly DbContext _entities;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<T> _dbset;

        protected GenericRepository(IDatabaseFactory factory, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _entities = factory.Get();
            _dbset = factory.Get().Set<T>();
        }

        public virtual T Add(T entity)
        {
            _dbset.Add(entity);
            _entities.Configuration.ValidateOnSaveEnabled = false;
            _unitOfWork.Commit();
            _entities.Configuration.ValidateOnSaveEnabled = true;
            return entity;
        }

        public virtual void AddPure(T entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbset.Attach(entity);
            _dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public void EditMany(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _entities.Entry(entity).State = EntityState.Modified;
            }
        }

        public IQueryable<T> Include(Expression<Func<T, Entity>> include)
        {
            return _dbset.Include(include);
        }

        public IQueryable<T> Include(Expression<Func<T, IEnumerable<Entity>>> include)
        {
            return _dbset.Include(include);
        }
        //Sync
        public int Count(Expression<Func<T, bool>> expression)
        {
            return _dbset.Count(expression);
        }

        public T Find(int id)
        {
            return _dbset.SingleOrDefault(x => x.Id == id);
        }

        public T First(Expression<Func<T, bool>> expression)
        {
            return _dbset.First(expression);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _dbset.FirstOrDefault(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbset;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression);
        }

        public int Count()
        {
            return _dbset.Count();
        }

        //Async

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbset.Where(expression).ToListAsync();
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbset.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbset.FirstAsync(expression);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbset.FirstOrDefaultAsync(expression);
        }
    }
}
