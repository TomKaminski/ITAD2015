using System.Linq;
using Itad2015.Contract.DTO.Base;
using Itad2015.Model.Common;

namespace Itad2015.Service.Helpers
{
    public static class MapperHelper<TPostDto,TEntity> 
        where TEntity:Entity
        where TPostDto:PostBaseDto
    {
        public static TEntity MapNoIdToEntityOnEdit(TPostDto model, TEntity entity)
        {
            foreach (var propertyInfo in model.GetType().GetProperties().Where(propertyInfo => propertyInfo.Name.ToLower() != "id"))
            {
                entity.GetType().GetProperty(propertyInfo.Name).SetValue(entity, propertyInfo.GetValue(model));
            }
            return entity;
        }
    }
}
