using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BooksRegistry.Contracts;
using FagkveldOktober.Models;
using FagkveldOktober.ServiceGateways;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sales.Contracts;
using SharedContracts;

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
            return View();
        }

        private ContentResult GetBook(int bookId)
        {
            var bookKey = new BookKey { Value = bookId };
            var bookInfo = _salesService.Execute(service => service.GetBooksAvailableForSale(new[] { bookKey })).First();
            var detail = _booksRegistryService.Execute(service => service.GetDetailsAboutBooks(new[] { bookKey })).First();

            var vm = new BookViewModel
            {
                Id = bookInfo.Id,
                PriceInOere = bookInfo.Price.PriceInOere,
                Title = detail.Title,
                Author = detail.Author,
                Category = detail.Category,
                Published = detail.Published,
                CoverFilename = detail.CoverFilename
            };
            var json = JsonConvert.SerializeObject(vm, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return Content(json, "application/json");
        }

        private ContentResult GetBooks()
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
            
            var json = JsonConvert.SerializeObject(booksAvailableForSale, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return Content(json, "application/json");
        }

        public ActionResult Books(int? bookId)
        {
            return bookId.HasValue ? GetBook(bookId.Value) : GetBooks();
        }
    }
}