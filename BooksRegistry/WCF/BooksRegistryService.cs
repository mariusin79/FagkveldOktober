using System.Collections.Generic;
using System.Linq;
using BooksRegistry.Automapping;
using BooksRegistry.Contracts;
using BooksRegistry.ReadModel;
using Raven.Client;
using Raven.Client.Linq;
using SharedContracts;

namespace BooksRegistry.WCF
{
    public class BooksRegistryService : IBooksRegistryService
    {
        private readonly IDocumentStore _database;
        private readonly IOurMapper _mapper;

        public BooksRegistryService(IDocumentStore database, IOurMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public List<Book> GetDetailsAboutBooks(BookKey[] books)
        {
            using (var session = _database.OpenSession())
            {
                return _mapper.Map<List<Book>>(session.Query<RmBook>().Where(book => book.Key.In(books)));
            }
        }
    }
}