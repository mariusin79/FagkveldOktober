using System.Collections.Generic;

namespace FagkveldOktober.Models
{
    public class PurchaseViewModel
    {
        public BookViewModel SelectedBook { get; set; }

        public IList<BookViewModel> AlsoBought { get; set; }
    }
}