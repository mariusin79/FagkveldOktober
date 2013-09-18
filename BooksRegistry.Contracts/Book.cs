using System;
using SharedContracts;

namespace BooksRegistry.Contracts
{
    public class Book
    {
        public BookKey Id { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public DateTime Published { get; set; }

        public string Category { get; set; }
    }
}
