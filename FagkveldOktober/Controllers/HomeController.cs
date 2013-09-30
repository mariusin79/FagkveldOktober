using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BooksRegistry.Contracts;
using FagkveldOktober.Models;
using FagkveldOktober.ServiceGateways;
using Sales.Contracts;

namespace FagkveldOktober.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceGateway<ISalesService> _salesService;
        private readonly IServiceGateway<IBooksRegistryService> _booksRegistryService;

        public HomeController(IServiceGateway<ISalesService> salesService, IServiceGateway<IBooksRegistryService> booksRegistryService)
        {
            _salesService = salesService;
            _booksRegistryService = booksRegistryService;
        }

        public ViewResult Index()
        {
            List<AvailableBook> availableBooks = _salesService.Execute(service => service.FindBooksAvailableForSale());
            List<Book> details = _booksRegistryService.Execute(service => service.GetDetailsAboutBooks(availableBooks.Select(book => book.Id).ToArray()));

            List<BookViewModel> booksAvailableForSale = availableBooks.Select(book => new BookViewModel
                {
                    Id = book.Id,
                    PriceInOere = book.Price.PriceInOere

                }).ToList();

            foreach (var detail in details)
            {
                BookViewModel bookViewModel = booksAvailableForSale.First(book => book.Id == detail.Id);

                bookViewModel.Title = detail.Title;
                bookViewModel.Author = detail.Author;
                bookViewModel.Category = detail.Category;
                bookViewModel.Published = detail.Published;
            }

            var vm = new HomeIndexViewModel
                {
                    BooksAvailableForSale = booksAvailableForSale
                };

            return View(vm);
        }
    }
}