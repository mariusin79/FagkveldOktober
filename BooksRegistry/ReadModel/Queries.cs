using System;
using System.Collections.Generic;
using BooksRegistry.Contracts;
using Raven.Client;
using Raven.Client.Linq;
using SharedContracts;

namespace BooksRegistry.ReadModel
{
    class Queries : IQueries
    {
        private readonly IDocumentStore _database;

        public Queries(IDocumentStore database)
        {
            _database = database;
        }

        public IEnumerable<Book> GetDetailsAboutBooks(BookKey[] books)
        {
            using (var session = _database.OpenSession())
            {
                return session.Query<Book>().Where(book => book.Id.In(books));
            }

        }
    }
}