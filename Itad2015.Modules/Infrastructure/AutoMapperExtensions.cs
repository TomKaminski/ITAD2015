using System.Linq;
using AutoMapper;

namespace Itad2015.Modules.Infrastructure
{
    public static class AutoMapperExtensions
    {
        public static void IgnoreNotExistingProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType == typeof(TSource) && x.DestinationType == typeof(TDestination));

            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
        }
    }
}
