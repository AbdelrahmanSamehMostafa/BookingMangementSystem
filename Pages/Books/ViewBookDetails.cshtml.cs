using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingMangementSystem.Pages.Books
{
    public class ViewBookDetails : PageModel
    {
        private readonly HttpClient _httpClient;

        public ViewBookDetails(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public BookWithAuthor BookWithAuthor { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            // Fetch the book details using the book ID
            var bookResponse = await _httpClient.GetAsync($"http://127.0.0.1:5284/api/Book/{id}");

            if (bookResponse.IsSuccessStatusCode)
            {
                var book = await bookResponse.Content.ReadAsAsync<Book>();

                // Fetch the author's name by the AuthorId from the book
                var authorResponse = await _httpClient.GetAsync($"http://127.0.0.1:5284/api/Author/{book.AuthorId}");

                string authorName = string.Empty;

                if (authorResponse.IsSuccessStatusCode)
                {
                    var author = await authorResponse.Content.ReadAsAsync<Author>();
                    authorName = author.Name;
                }

                // Set the book and author details
                BookWithAuthor = new BookWithAuthor
                {
                    Book = book,
                    AuthorName = authorName
                };

                return Page(); // Return the page with the fetched data
            }
            else
            {
                // Handle the case where the book cannot be found (e.g., return NotFound page)
                return NotFound();
            }
        }
    }
}