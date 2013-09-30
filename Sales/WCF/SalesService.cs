using System.Collections.Generic;
using Sales.Automapping;
using Sales.Contracts;
using Sales.ReadModel;
using SharedContracts;

namespace Sales.WCF
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

        public List<AvailableBook> GetBooksAvailableForSale(BookKey[] books)
        {
            var result = _queries.GetBooksAvailableForSale(books);

            return _mapper.Map<List<AvailableBook>>(result);
        }
    }
}