using System.Collections.Generic;
using SharedContracts;

namespace Sales.PlaceAnOrder
{
    public class CustomerSales
    {
        public virtual int Id { get; set; }

        public virtual CustomerKey CustomerKey { get; set; }

        public virtual IList<SoldBook> SoldBooks { get; set; }

        public virtual void PlaceAnOrder(BookKey[] booksIAmBuying)
        {
            foreach (var bookToBuy in booksIAmBuying)
            {
                SoldBooks.Add(new SoldBook {SoldBookKey = bookToBuy});
            }
        }
    }
}