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
    public class ViewBookDetails : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public ViewBookDetails(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _bookRepository.GetBookByIdAsync(id);
            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}