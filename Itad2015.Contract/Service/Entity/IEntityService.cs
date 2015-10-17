using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.Service.Entity
{
    public interface IEntityService<TGetDto, TPostDto>
        where TGetDto : GetBaseDto
        where TPostDto : PostBaseDto
    {
        void Delete(int id);
        SingleServiceResult<TGetDto> Get(int id);
        void Edit(TPostDto entity);

        void EditMany(IList<TPostDto> objs);

        SingleServiceResult<TGetDto> Create(TPostDto entity);
        void CreateMany(IEnumerable<TPostDto> entities);
        MultipleServiceResult<TGetDto> GetAll();
        MultipleServiceResult<TGetDto> GetAll(Expression<Func<TGetDto, bool>> predicate);

        SingleServiceResult<TGetDto> FirstOrDefault(Expression<Func<TGetDto, bool>> predicate);

        int Count();

        int Count(Expression<Func<TGetDto, bool>> predicate);

        //Async
        Task<SingleServiceResult<TGetDto>> GetAsync(int id);
        Task<SingleServiceResult<TGetDto>> CreateAsync(TPostDto entity);
        Task<MultipleServiceResult<TGetDto>> GetAllAsync();
        Task<MultipleServiceResult<TGetDto>> GetAllAsync(Expression<Func<TGetDto, bool>> predicate);
        Task EditAsync(TPostDto entity);
        Task DeleteAsync(int id);
    }
}
