using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Itad2015.Contract.DTO.Base;

namespace Itad2015.Contract.Service.Entity
{
    public interface IEntityService<TGetDto, TPostDto>
        where TGetDto : GetBaseDto
        where TPostDto : PostBaseDto
    {
        void Delete(int id);
        TGetDto Get(int id);
        void Edit(TPostDto entity);
        void Create(TPostDto entity);
        IEnumerable<TGetDto> GetAll();
        IEnumerable<TGetDto> GetAll(Expression<Func<TGetDto, bool>> predicate);



        //Async
        Task<TGetDto> GetAsync(int id);
        Task CreateAsync(TPostDto entity);
        Task<IEnumerable<TGetDto>> GetAllAsync();
        Task<IEnumerable<TGetDto>> GetAllAsync(Expression<Func<TGetDto, bool>> predicate);
        Task EditAsync(TPostDto entity);
        Task DeleteAsync(int id);
    }
}
