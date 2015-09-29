using System.Threading.Tasks;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;

namespace Itad2015.Contract.Service.Entity
{
    public interface IUserService : IEntityService<UserGetDto, UserPostDto>
    {
        Task<SingleServiceResult<bool>> LoginAsync(string email, string password);
        SingleServiceResult<bool> Login(string email, string password);

        Task<SingleServiceResult<UserGetDto>> GetByEmailAsync(string email);
        SingleServiceResult<UserGetDto> GetByEmail(string email);
    }
}
