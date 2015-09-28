using System.Threading.Tasks;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Contract.Service.Entity;
using Itad2015.Model.Concrete;
using Itad2015.Repository.Common;
using Itad2015.Repository.Interfaces;

namespace Itad2015.Service.Concrete
{
    public class WorkshopService : EntityService<WorkshopGetDto, WorkshopPostDto, Workshop>, IWorkshopService
    {
        private readonly IWorkshopRepository _repository;
        private readonly IImageProcessorService _imageProcessorService;
        private readonly IUnitOfWork _unitOfWork;

        public WorkshopService(IUnitOfWork unitOfWork, IWorkshopRepository repository, IImageProcessorService imageProcessorService) : base(unitOfWork, repository)
        {
            _repository = repository;
            _imageProcessorService = imageProcessorService;
            _unitOfWork = unitOfWork;
        }

        public void Delete(int id, string path)
        {
            var obj = _repository.Find(id);
            _imageProcessorService.DeleteImagesByPath(path+obj.Title);
            _repository.Delete(obj);
            _unitOfWork.Commit();
        }
    }
}
