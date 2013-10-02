using System.Collections.Generic;
using AutoMapper;

namespace Sales.Automapping
{
    public class Mapper : IMapper
    {
        public Mapper(IList<IMappingConfiguration> configurations)
        {
            foreach (var mappingConfiguration in configurations)
            {
                mappingConfiguration.Setup();
            }
        }

        public T Map<T>(object src)
        {
            return AutoMapper.Mapper.Map<T>(src);
        } 
    }
}