using System;
using System.ServiceModel;
using BooksRegistry.Contracts;
using BooksRegistry.ReadModel;
using NServiceStub.Configuration;
using Raven.Client;
using Raven.Client.Document;
using Sales.Contracts;
using SharedContracts;
using NServiceStub.NServiceBus;

namespace Testing
{
    public class Class1
    {
        public void TryToSayHelloToTheSalesService()
        {
            using (var factory = new ChannelFactory<ISalesService>(new BasicHttpBinding(), "http://localhost:9101/sales"))
            {
                ISalesService service = factory.CreateChannel();

                service.FindBooksAvailableForSale();
            }
        }

        public void TellMarketingThatABookHasBeenSold()
        {
            var stub = Configure.Stub().NServiceBusSerializers().Create("servicehost");

            stub.Setup().Send<IBooksWereSold>(@event =>
                {
                    @event.Books = new[]
                        {
                            new BookKey {Value = 1}, 
                            new BookKey {Value = 2},
                            new BookKey {Value = 3}
                        };
                }, "servicehost");

            stub.Start();

            stub.Stop();
        }

        public void AddSomeBooksToTheRegistry()
        {
            using (IDocumentStore db = new DocumentStore { Url = "http://localhost:8080", DefaultDatabase = "BooksRegistry" })
            {
                db.Initialize();

                IDocumentSession session = db.OpenSession();

                AddBook(session, "George Orwell", "Paperback", "1984.jpg", 1, "1984", new DateTime(2010, 11, 28));
                AddBook(session, "J. R. R. Tolkien", "Paperback", "thehobbit.jpg", 2, "The Hobbit: Illustrated Edition", new DateTime(2009, 04, 20));
                AddBook(session, "Joel Friel", "Kindle Edition", "cyclists-training-bible.jpg", 3, "The Cyclist's Training Bible", new DateTime(2012, 11, 27));

                session.SaveChanges();
            }
        }

        private static void AddBook(IDocumentSession session, string author, string category, string coverFilename, int id, string title, DateTime published)
        {
            var book = new RmBook
                {
                    Author = author,
                    Category = category,
                    Key = new BookKey { Value = id },
                    Title = title,
                    Published = published,
                    CoverFilename = coverFilename
                };

            session.Store(book);
        }
    }
}
