using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingMangementSystem.Pages.Authors
{
    public class ViewAllAuthors : PageModel
    {
        private readonly HttpClient _httpClient;

        public ViewAllAuthors(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<BooksSystem.Models.Author> Authors { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch the list of books from the backend API
            var response = await _httpClient.GetAsync("http://localhost:5284/api/Author");

            if (response.IsSuccessStatusCode)
            {
                Authors = await response.Content.ReadAsAsync<List<BooksSystem.Models.Author>>();
            }
            else
            {
                Authors = new List<BooksSystem.Models.Author>(); // Handle the error accordingly (e.g., log the issue)
            }
        }
    }
}