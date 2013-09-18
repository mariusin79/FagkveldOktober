using System.Collections.Generic;
using AutoMapper;
using Sales.Automapping;
using Sales.Contracts;
using Sales.ReadModel;

namespace Sales
{
    public class SalesService : ISalesService
    {
        private readonly IQueries _queries;
        private readonly IOurMapper _mapper;

        public SalesService(IQueries queries, IOurMapper mapper)
        {
            _queries = queries;
            _mapper = mapper;
        }

        public List<AvailableBook> FindBooksAvailableForSale()
        {
            var books = _queries.FindBooksAvailableForSale();

            return _mapper.Map<List<AvailableBook>>(books);
        }
    }
}