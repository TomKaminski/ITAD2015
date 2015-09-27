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
        protected readonly IDbSet<T> Dbset;

        protected GenericRepository(IDatabaseFactory factory)
        {
            _entities = factory.Get();
            Dbset = factory.Get().Set<T>();
        }

        public void Add(T entity)
        {
            Dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            Dbset.Attach(entity);
            Dbset.Remove(entity);
        }

        public void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }


        //Sync
        public T Find(int id)
        {
            return Dbset.SingleOrDefault(x => x.Id == id);
        }

        public T First(Expression<Func<T, bool>> expression)
        {
            return Dbset.First(expression);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return Dbset.FirstOrDefault(expression);
        }

        public IQueryable<T> GetAll()
        {
            return Dbset;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return Dbset.Where(expression);
        }

        //Async

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Dbset.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await Dbset.Where(expression).ToListAsync();
        }

        public async Task<T> FindAsync(int id)
        {
            return await Dbset.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression)
        {
            return await Dbset.FirstAsync(expression);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await Dbset.FirstOrDefaultAsync(expression);
        }
    }
}
