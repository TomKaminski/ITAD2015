using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Itad2015.Contract.DTO.Base;
using Itad2015.Contract.Service;
using Itad2015.Contract.Service.Entity;
using Itad2015.Model.Common;
using Itad2015.Repository.Common;
using Itad2015.Service.Helpers;

namespace Itad2015.Service.Concrete
{
    public class EntityService<TGetDto, TPostDto, TEntity> : IEntityService<TGetDto, TPostDto>
        where TGetDto : GetBaseDto
        where TPostDto : PostBaseDto
        where TEntity : Entity
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        protected EntityService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }



        public void Delete(int id)
        {
            var obj = _repository.Find(id);
            _repository.Delete(obj);
            _unitOfWork.Commit();
        }

        public TGetDto Get(int id)
        {
            return (TGetDto)Mapper.Map(_repository.Find(id), typeof(TEntity), typeof(TGetDto));
        }

        public void Edit(TPostDto entity)
        {
            var obj = _repository.Find(entity.Id);
            if (obj == null) return;
            obj = MapperHelper<TPostDto, TEntity>.MapNoIdToEntityOnEdit(entity, obj);
            _repository.Edit(obj);
            _unitOfWork.Commit();
        }

        public void Create(TPostDto entity)
        {
            _repository.Add(Mapper.Map<TEntity>(entity));
            _unitOfWork.Commit();
        }

        public IEnumerable<TGetDto> GetAll()
        {
            return _repository.GetAll().Select(Mapper.Map<TGetDto>).ToList();
        }

        public IEnumerable<TGetDto> GetAll(Expression<Func<TGetDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var result = new CustomExpressionVisitor<TEntity>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(result, param);
            return _repository.GetAll(lambda).Select(Mapper.Map<TGetDto>).ToList();
        }

        //Async

        public async Task<TGetDto> GetAsync(int id)
        {
            return (TGetDto)Mapper.Map(await _repository.FindAsync(id), typeof(TEntity), typeof(TGetDto));
        }

        public async Task CreateAsync(TPostDto entity)
        {
            _repository.Add(Mapper.Map<TEntity>(entity));
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TGetDto>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).Select(Mapper.Map<TGetDto>).ToList();
        }

        public async Task<IEnumerable<TGetDto>> GetAllAsync(Expression<Func<TGetDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var result = new CustomExpressionVisitor<TEntity>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(result, param);
            return (await _repository.GetAllAsync(lambda)).Select(Mapper.Map<TGetDto>).ToList();
        }

        public async Task EditAsync(TPostDto entity)
        {
            var obj = await _repository.FindAsync(entity.Id);
            if (obj == null) return;
            obj = MapperHelper<TPostDto, TEntity>.MapNoIdToEntityOnEdit(entity, obj);
            _repository.Edit(obj);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var obj = await _repository.FindAsync(id);
            _repository.Delete(obj);
            await _unitOfWork.CommitAsync();
        }
    }
}
