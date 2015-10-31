using System.Threading.Tasks;
using AutoMapper;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;
using Itad2015.Service.Helpers;
using Itad2015.Service.Helpers.Interfaces;

namespace Itad2015.Service.Concrete
{
    public class UserService : EntityService<UserGetDto, UserPostDto,User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
                
        public UserService(IUnitOfWork unitOfWork, IUserRepository repository, IPasswordHasher passwordHasher) : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<SingleServiceResult<bool>> LoginAsync(string email, string password)
        {
            var user = await _repository.FirstOrDefaultAsync(x => x.Email == email);
            var obj = user != null && _passwordHasher.ValidatePassword(password, user.PasswordHash, user.PasswordSalt);
            return new SingleServiceResult<bool>(obj);
        }

        public SingleServiceResult<bool> Login(string email, string password)
        {
            var user = _repository.FirstOrDefault(x => x.Email == email);
            var obj = user != null && _passwordHasher.ValidatePassword(password, user.PasswordHash, user.PasswordSalt);
            return new SingleServiceResult<bool>(obj);
        }

        public async Task<SingleServiceResult<UserGetDto>> GetByEmailAsync(string email)
        {
            var user = await _repository.FirstOrDefaultAsync(x => x.Email == email);
            var obj = user == null ? null : Mapper.Map<UserGetDto>(user);
            return new SingleServiceResult<UserGetDto>(obj);
        }

        public SingleServiceResult<UserGetDto> GetByEmail(string email)
        {
            var user = _repository.FirstOrDefault(x => x.Email == email);
            var obj = user == null ? null : Mapper.Map<UserGetDto>(user);
            return new SingleServiceResult<UserGetDto>(obj);
        }

        public override SingleServiceResult<UserGetDto> Create(UserPostDto entity)
        {
            var saltHash = _passwordHasher.CreateHash(entity.Password);
            char[] delimiter = { ':' };
            var split = saltHash.Split(delimiter);
            var salt = split[0];
            var hash = split[1];

            var mappedEntity = Mapper.Map<User>(entity);
            mappedEntity.PasswordSalt = salt;
            mappedEntity.PasswordHash = hash;

            var obj = _repository.Add(mappedEntity);
            _unitOfWork.Commit();
            return new SingleServiceResult<UserGetDto>(Mapper.Map<UserGetDto>(obj));
        }

        public override void Edit(UserPostDto entity)
        {
            var obj = Mapper.Map<User>(_repository.Find(entity.Id));
            if (obj == null) return;
            if (entity.Password != string.Empty)
            {
                var saltHash = _passwordHasher.CreateHash(entity.Password);
                char[] delimiter = { ':' };
                var split = saltHash.Split(delimiter);
                var salt = split[0];
                var hash = split[1];
                obj.PasswordHash = hash;
                obj.PasswordSalt = salt;
            }
            _repository.Edit(obj);
            _unitOfWork.Commit();
        }
    }
}
