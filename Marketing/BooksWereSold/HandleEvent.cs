using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using Neo4jClient;
using Neo4jClient.Cypher;
using Sales.Contracts;
using SharedContracts;

namespace Marketing.BooksWereSold
{
    public class HandleEvent : IHandleMessages<IBooksWereSold>
    {
        private readonly IGraphClient _client;

        public HandleEvent(IGraphClient client)
        {
            _client = client;
        }

        public void Handle(IBooksWereSold message)
        {
            List<Node<Book>> books = GetOrCreateBookNodes(message);

            RegisterThatTheBooksHaveBeenSoldTogether(books);
        }

        private void RegisterThatTheBooksHaveBeenSoldTogether(List<Node<Book>> books)
        {
            for (int i = 0; i < books.Count - 1; i++)
            {
                var targetNode = books[i];

                var otherNodes = books.Skip(i + 1).Select(n => n.Reference.Id.ToString()).ToArray();
                var otherNodesJoined = string.Join(",", otherNodes);

                var addMissingEdgesQuery = string.Format(
                    "g.v({0})_().filter{{it.both('{1}').has('id', {2}L).count() == 0}}.each{{g.addEdge(g.v({2}), it, '{1}', ['Count':0])}}",
                    otherNodesJoined,
                    AlsoBoughtWith.TypeKey,
                    targetNode.Reference.Id
                    );
                _client.ExecuteScalarGremlin(addMissingEdgesQuery, null);

                var incrementCountQuery = string.Format(
                    "g.v({0})_().bothE('{1}').filter{{it.inVertex.id == {2} || it.outVertex.id == {2}}}.sideEffect{{it.Count++}}",
                    otherNodesJoined,
                    AlsoBoughtWith.TypeKey,
                    targetNode.Reference.Id
                    );
                _client.ExecuteScalarGremlin(incrementCountQuery, null);
            }

        }

        private List<Node<Book>> GetOrCreateBookNodes(IBooksWereSold message)
        {
            var allBooks = new List<Node<Book>>();

            foreach (var b in message.Books)
            {
                var query = new CypherQuery(@"start n=node(*) where has(n.Id) and n.Id = {p0} return n", new Dictionary<string, object> {{"p0", b.Value}}, CypherResultMode.Set);
                var books = ((IRawGraphClient) _client).ExecuteGetCypherResults<Node<Book>>(query).ToList();

                if (!books.Any())
                {
                    NodeReference<Book> nodeReference = _client.Create(new Book {Id = b.Value});
                    allBooks.Add(_client.Get(nodeReference));
                }
                else
                    allBooks.AddRange(books);
            }

            return allBooks;
        }
    }
}