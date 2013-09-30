using AutoMapper;
using BooksRegistry.Automapping;
using BooksRegistry.ReadModel;

namespace BooksRegistry.WCF
{
    public class MappingConfiguration : IMappingConfiguration
    {
        public void Setup()
        {
            Mapper.CreateMap<RmBook, Contracts.Book>().ForMember(dst => dst.Id, opt => opt.ResolveUsing(src => src.Key));
        }
    }
}