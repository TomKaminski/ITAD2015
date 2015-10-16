using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Service.Concrete
{
    public class InvitedPersonService : EntityService<InvitedPersonGetDto,InvitedPersonPostDto,InvitedPerson>,IInvitedPersonService
    {
        public InvitedPersonService(IUnitOfWork unitOfWork, IInvitedPersonRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
