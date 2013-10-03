using System;
using SharedContracts;

namespace FagkveldOktober.Models
{
    public class CartItemViewModel
    {
        public BookKey Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public DateTime Published { get; set; }

        public int Quantity { get; set; }

        public long SumTotalInOere { get; set; }
    }
}