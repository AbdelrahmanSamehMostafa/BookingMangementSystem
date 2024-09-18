using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BooksSystem.Pages.Book
{
    public class ViewAllBooks : PageModel
    {
        private readonly HttpClient _httpClient;

        public ViewAllBooks(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public List<BookWithAuthor> BooksWithAuthors { get; set; } = new List<BookWithAuthor>();

        public async Task OnGetAsync()
        {

            // Fetch the list of books from the backend API
            var bookResponse = await _httpClient.GetAsync("http://localhost:5284/api/Book");

            if (bookResponse.IsSuccessStatusCode)
            {
                var books = await bookResponse.Content.ReadAsAsync<List<BookingMangementSystem.Models.Book>>();

                // Loop through each book to fetch the author's name
                foreach (var book in books)
                {
                    // Call the API to get the author's name using the AuthorId
                    var authorResponse = await _httpClient.GetAsync($"http://127.0.0.1:5284/api/Author/{book.AuthorId}");

                    string authorName = string.Empty;

                    if (authorResponse.IsSuccessStatusCode)
                    {
                        var author = await authorResponse.Content.ReadAsAsync<Author>();
                        authorName = author.Name;
                    }

                    // Add the book and author information to the BooksWithAuthors list
                    BooksWithAuthors.Add(new BookWithAuthor
                    {
                        Book = book,
                        AuthorName = authorName
                    });
                }
            }
            else
            {
                // Handle the case where books cannot be fetched (e.g., log error)
                BooksWithAuthors = new List<BookWithAuthor>();
            }
        }
    }
}