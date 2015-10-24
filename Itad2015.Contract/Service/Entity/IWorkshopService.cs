using System.Collections.Generic;
using System.Threading.Tasks;
using Itad2015.Contract.Common;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;

namespace Itad2015.Contract.Service.Entity
{
    public interface IWorkshopService : IEntityService<WorkshopGetDto, WorkshopPostDto>
    {
        void Delete(int id, string path);

        SingleServiceResult<IEnumerable<WorkshopGuestListGetDto>> GetWorkshopGuestsGrouped();
    }
}
