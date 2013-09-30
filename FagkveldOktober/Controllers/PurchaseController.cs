using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BooksRegistry.Contracts;
using FagkveldOktober.Models;
using FagkveldOktober.ServiceGateways;
using Marketing.Contracts;
using Sales.Contracts;
using SharedContracts;

namespace FagkveldOktober.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IServiceGateway<IBooksRegistryService> _bookDetailsProvider;
        private readonly IServiceGateway<ISalesService> _priceProvider;
        private readonly IServiceGateway<IMarketingService> _marketingInfoProvider; 

        public PurchaseController(IServiceGateway<IBooksRegistryService> bookDetailsProvider, IServiceGateway<ISalesService> priceProvider, IServiceGateway<IMarketingService> marketingInfoProvider)
        {
            _bookDetailsProvider = bookDetailsProvider;
            _priceProvider = priceProvider;
            _marketingInfoProvider = marketingInfoProvider;
        }

        public ActionResult View(int bookId)
        {
            var selectedBookKey = new BookKey {Value = bookId};

            var alsoBought = _marketingInfoProvider.Execute(service => service.FindBooksWhoPeopleAlsoBoughtWhenTheyBought(selectedBookKey)).Select(b => b.Id).ToList();

            var allIds = new List<BookKey>(alsoBought);
            allIds.Add(selectedBookKey);

            var bookDetails = _bookDetailsProvider.Execute(service => service.GetDetailsAboutBooks(allIds.ToArray()));
            var prices = _priceProvider.Execute(service => service.GetBooksAvailableForSale(allIds.ToArray()));

            var selectedBookDetails = bookDetails.First(book => book.Id == selectedBookKey);

            var vm = new PurchaseViewModel
                {
                    SelectedBook = new BookViewModel
                        {
                            Author = selectedBookDetails.Author,
                            Category = selectedBookDetails.Category,
                            Id = selectedBookDetails.Id,
                            PriceInOere = prices.First(price => price.Id == selectedBookKey).Price.PriceInOere,
                            Published = selectedBookDetails.Published,
                            Title = selectedBookDetails.Title
                        },
                        AlsoBought = bookDetails.Where(book => book.Id != selectedBookKey).Select(details => new BookViewModel
                            {
                                Author = details.Author,
                                Category = details.Category,
                                Id = details.Id, 
                                PriceInOere = prices.First(price => price.Id == details.Id).Price.PriceInOere, 
                                Published = details.Published,
                                Title = details.Title
                            }).ToList()
                };

            return View(vm);
         }
    }
}