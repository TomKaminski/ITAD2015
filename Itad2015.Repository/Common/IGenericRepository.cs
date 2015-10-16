using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Itad2015.Repository.Common
{
    public interface IGenericRepository<T>
    {
        T Add(T entity);
        void AddPure(T entity);
        void Delete(T entity);
        void Edit(T entity);

        void EditMany(IEnumerable<T> entities);

        //Sync
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T,bool>> expression);

        int Count();
        int Count(Expression<Func<T, bool>> expression);

        T Find(int id);
        T First(Expression<Func<T, bool>> expression);
        T FirstOrDefault(Expression<Func<T, bool>> expression);


        //Async
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        Task<T> FindAsync(int id);
        Task<T> FirstAsync(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    }
}
