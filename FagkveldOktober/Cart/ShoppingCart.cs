using System;
using System.Collections.Generic;
using SharedContracts;

namespace FagkveldOktober.Cart
{
    [Serializable]
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = Items ?? new Dictionary<int, BookOrder>();
        }

        public void AddToCart(BookKey book)
        {
            if (!Items.ContainsKey(book.Value))
                Items.Add(book.Value, new BookOrder(book));
            else
            {
                Items[book.Value].Quantity++;
            }
        }

        public Dictionary<int, BookOrder> Items { get; set; }

    }
}