using System.Collections.Generic;
using AutoMapper;

namespace BooksRegistry.Automapping
{
    public class OurMapper : IOurMapper
    {
        public OurMapper(IList<IMappingConfiguration> configurations)
        {
            foreach (var mappingConfiguration in configurations)
            {
                mappingConfiguration.Setup();
            }
        }

        public T Map<T>(object src)
        {
            return Mapper.Map<T>(src);
        } 
    }
}