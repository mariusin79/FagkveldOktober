using System.Collections.Generic;
using System.Linq;
using Marketing.BooksWereSold;
using Marketing.Contracts;
using Neo4jClient;
using Neo4jClient.Cypher;
using SharedContracts;

namespace Marketing.WCF
{
    public class MarketingService : IMarketingService
    {
        private IGraphClient _client;

        public MarketingService(IGraphClient client)
        {
            _client = client;
        }

        public List<SoldBook> FindBooksWhoPeopleAlsoBoughtWhenTheyBought(BookKey book)
        {
            var query = new CypherQuery(@"start n=node(*) where has(n.Id) and n.Id = {p0} return n", new Dictionary<string, object> {{"p0", book.Value}}, CypherResultMode.Set);

            var books = ((IRawGraphClient) _client).ExecuteGetCypherResults<Node<Book>>(query).ToList();

            IEnumerable<Node<Book>> soldBooks = _client.Cypher.Start(new {n = books}).Match("(n)--(x)").Return<Node<Book>>("x").Results;

            return soldBooks.Select(bookReference => new SoldBook{ Id = new BookKey{ Value = bookReference.Data.Id }}).ToList();
        }
    }
}