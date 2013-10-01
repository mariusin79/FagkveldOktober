using System;
using SharedContracts;

namespace FagkveldOktober.Cart
{
    [Serializable]
    public class BookOrder
    {
        public BookOrder(BookKey book)
        {
            Book = book;
            Quantity = 1;
        }

        public BookKey Book { get; set; }

        public int Quantity { get; set; }
    }
}