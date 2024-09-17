using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<BooksSystem.Models.Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch the list of books from the backend API
            var response = await _httpClient.GetAsync("http://localhost:5284/api/Book");

            if (response.IsSuccessStatusCode)
            {
                Books = await response.Content.ReadAsAsync<List<BooksSystem.Models.Book>>();
            }
            else
            {
                Books = new List<BooksSystem.Models.Book>(); // Handle the error accordingly (e.g., log the issue)
            }
        }
    }
}