using System;
using SharedContracts;

namespace BooksRegistry.ReadModel
{
    public class RmBook
    {
        public BookKey Key { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public DateTime Published { get; set; }

        public string Category { get; set; }
 
    }
}