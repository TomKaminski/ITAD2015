using System.Threading.Tasks;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;

namespace Itad2015.Contract.Service.Entity
{
    public interface IUserService : IEntityService<UserGetDto, UserPostDto>
    {
        Task<bool> LoginAsync(string email, string password);
        bool Login(string email, string password);

        Task<UserGetDto> GetByEmailAsync(string email);
        UserGetDto GetByEmail(string email);
    }
}
