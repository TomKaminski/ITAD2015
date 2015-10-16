using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.Base;
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



        public virtual void Delete(int id)
        {
            var obj = _repository.Find(id);
            _repository.Delete(obj);
            _unitOfWork.Commit();
        }

        public virtual SingleServiceResult<TGetDto> Get(int id)
        {
            var obj = (TGetDto)Mapper.Map(_repository.Find(id), typeof(TEntity), typeof(TGetDto));
            return new SingleServiceResult<TGetDto>(obj);
        }

        public virtual void Edit(TPostDto entity)
        {
            var obj = _repository.Find(entity.Id);
            if (obj == null) return;
            obj = MapperHelper<TPostDto, TEntity>.MapNoIdToEntityOnEdit(entity, obj);
            _repository.Edit(obj);
            _unitOfWork.Commit();
        }

        public virtual void EditMany(IEnumerable<TPostDto> objs)
        {
            var entities = _repository.GetAll(x => objs.Select(k => k.Id).Contains(x.Id)).ToList();

            foreach (var t in objs)
            {
                var entity = entities.Single(x => x.Id == t.Id);
                entity = MapperHelper<TPostDto, TEntity>.MapNoIdToEntityOnEdit(t, entity);
                _repository.Edit(entity);
            }
            _unitOfWork.Commit();
        }

        public virtual void CreateMany(IEnumerable<TPostDto> entities)
        {
            foreach (var entity in entities)
            {
                _repository.AddPure(Mapper.Map<TEntity>(entity));
            }
            _unitOfWork.Commit();
        }

        public virtual SingleServiceResult<TGetDto> Create(TPostDto entity)
        {
            
            var obj =_repository.Add(Mapper.Map<TEntity>(entity));
            _unitOfWork.Commit();
            return new SingleServiceResult<TGetDto>(Mapper.Map<TGetDto>(obj));
        }

        public virtual MultipleServiceResult<TGetDto> GetAll()
        {
            var obj = _repository.GetAll().Select(Mapper.Map<TGetDto>).ToList();
            return new MultipleServiceResult<TGetDto>(obj);
        }

        public virtual MultipleServiceResult<TGetDto> GetAll(Expression<Func<TGetDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var result = new CustomExpressionVisitor<TEntity>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(result, param);
            var obj = _repository.GetAll(lambda).Select(Mapper.Map<TGetDto>).ToList();
            return new MultipleServiceResult<TGetDto>(obj);
        }


        public SingleServiceResult<TGetDto> FirstOrDefault(Expression<Func<TGetDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var result = new CustomExpressionVisitor<TEntity>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(result, param);
            var obj = Mapper.Map<TGetDto>(_repository.FirstOrDefault(lambda));
            return new SingleServiceResult<TGetDto>(obj);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public int Count(Expression<Func<TGetDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var result = new CustomExpressionVisitor<TEntity>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(result, param);
            return _repository.Count(lambda);
        }

        //Async

        public virtual async Task<SingleServiceResult<TGetDto>> GetAsync(int id)
        {
            var obj = (TGetDto)Mapper.Map(await _repository.FindAsync(id), typeof(TEntity), typeof(TGetDto));
            return new SingleServiceResult<TGetDto>(obj);
        }

        public virtual async Task<SingleServiceResult<TGetDto>> CreateAsync(TPostDto entity)
        {
            var obj = _repository.Add(Mapper.Map<TEntity>(entity));
            await _unitOfWork.CommitAsync();
            return new SingleServiceResult<TGetDto>(Mapper.Map<TGetDto>(obj));
        }

        public virtual async Task<MultipleServiceResult<TGetDto>> GetAllAsync()
        {
            var obj = (await _repository.GetAllAsync()).Select(Mapper.Map<TGetDto>).ToList();
            return new MultipleServiceResult<TGetDto>(obj);
        }

        public virtual async Task<MultipleServiceResult<TGetDto>> GetAllAsync(Expression<Func<TGetDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var result = new CustomExpressionVisitor<TEntity>(param).Visit(predicate.Body);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(result, param);
            var obj = (await _repository.GetAllAsync(lambda)).Select(Mapper.Map<TGetDto>).ToList();
            return new MultipleServiceResult<TGetDto>(obj);
        }

        public virtual async Task EditAsync(TPostDto entity)
        {
            var obj = await _repository.FindAsync(entity.Id);
            if (obj == null) return;
            obj = MapperHelper<TPostDto, TEntity>.MapNoIdToEntityOnEdit(entity, obj);
            _repository.Edit(obj);
            await _unitOfWork.CommitAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var obj = await _repository.FindAsync(id);
            _repository.Delete(obj);
            await _unitOfWork.CommitAsync();
        }
    }
}
