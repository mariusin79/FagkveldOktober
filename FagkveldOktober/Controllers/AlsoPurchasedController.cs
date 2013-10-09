using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BooksRegistry.Contracts;
using FagkveldOktober.Models;
using FagkveldOktober.ServiceGateways;
using Marketing.Contracts;
using Sales.Contracts;
using SharedContracts;

namespace FagkveldOktober.Controllers
{
    public class AlsoPurchasedController : ApiController
    {
        private readonly IServiceGateway<IBooksRegistryService> _bookDetailsProvider;
        private readonly IServiceGateway<ISalesService> _priceProvider;
        private readonly IServiceGateway<IMarketingService> _marketingInfoProvider;

        public AlsoPurchasedController(IServiceGateway<IBooksRegistryService> bookDetailsProvider, IServiceGateway<ISalesService> priceProvider, IServiceGateway<IMarketingService> marketingInfoProvider)
        {
            _bookDetailsProvider = bookDetailsProvider;
            _priceProvider = priceProvider;
            _marketingInfoProvider = marketingInfoProvider;
        }

        public IEnumerable<BookViewModel> Get(int bookId)
        {
            var selectedBookKey = new BookKey { Value = bookId };
            var alsoBought = _marketingInfoProvider.Execute(service => service.FindBooksWhoPeopleAlsoBoughtWhenTheyBought(selectedBookKey)).Select(b => b.Id).ToList();

            var bookDetails = _bookDetailsProvider.Execute(service => service.GetDetailsAboutBooks(alsoBought.ToArray()));
            var prices = _priceProvider.Execute(service => service.GetBooksAvailableForSale(alsoBought.ToArray()));

            var alsoPurchased = bookDetails.Select(details => new BookViewModel
            {
                Author = details.Author,
                Category = details.Category,
                Id = details.Id,
                PriceInOere = prices.First(price => price.Id == details.Id).Price.PriceInOere,
                Published = details.Published,
                Title = details.Title,
                CoverFilename = details.CoverFilename
            }).ToList();
            return alsoPurchased;
        }
    }
}
