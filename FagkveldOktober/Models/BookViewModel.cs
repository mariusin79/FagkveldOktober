using System;
using SharedContracts;

namespace FagkveldOktober.Models
{
    public class BookViewModel
    {
        public BookKey Id { get; set; }
        
        public long PriceInOere { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public DateTime Published { get; set; }
    }
}