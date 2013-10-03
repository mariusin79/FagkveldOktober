using System.Collections.Generic;

namespace FagkveldOktober.Models
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Content { get; set; }

        public long SumTotalInOere { get; set; }
    }
}