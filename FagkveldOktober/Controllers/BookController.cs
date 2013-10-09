using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BooksRegistry.Contracts;
using FagkveldOktober.Models;
using FagkveldOktober.ServiceGateways;
using Sales.Contracts;

namespace FagkveldOktober.Controllers
{
    public class BookController : ApiController
    {
        private readonly IServiceGateway<ISalesService> _salesService;
        private readonly IServiceGateway<IBooksRegistryService> _booksRegistryService;

        public BookController(IServiceGateway<ISalesService> salesService, IServiceGateway<IBooksRegistryService> booksRegistryService)
        {
            _salesService = salesService;
            _booksRegistryService = booksRegistryService;
        }

        public IEnumerable<BookViewModel> Get()
        {
            var availableBooks = _salesService.Execute(service => service.FindBooksAvailableForSale());
            var bookDetails = _booksRegistryService.Execute(service => service.GetDetailsAboutBooks(availableBooks.Select(book => book.Id).ToArray()));

            List<BookViewModel> booksAvailableForSale = availableBooks.Select(book => new BookViewModel
            {
                Id = book.Id,
                PriceInOere = book.Price.PriceInOere
            }).ToList();

            foreach (var detail in bookDetails)
            {
                BookViewModel bookViewModel = booksAvailableForSale.First(book => book.Id == detail.Id);

                bookViewModel.Title = detail.Title;
                bookViewModel.Author = detail.Author;
                bookViewModel.Category = detail.Category;
                bookViewModel.Published = detail.Published;
                bookViewModel.CoverFilename = detail.CoverFilename;
            }
            return booksAvailableForSale;
        }
    }
}
