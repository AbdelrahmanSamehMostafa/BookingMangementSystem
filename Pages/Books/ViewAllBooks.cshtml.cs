using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
using BookingMangementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingMangementSystem.Pages.Books
{
    public class ViewAllBooks : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public ViewAllBooks(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> Books { get; set; }

        public string SuccessMessage { get; set; }


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool? IsRecommended { get; set; }
        

        [BindProperty(SupportsGet = true)]
        public bool? HasFile { get; set; }

        public async Task OnGetAsync()
        {
            // Example code to set a success message if needed
            SuccessMessage = Request.Query["message"];

            Books = await _bookRepository.SearchBooksAsync(SearchTerm, IsRecommended, HasFile);
        }
    }
}