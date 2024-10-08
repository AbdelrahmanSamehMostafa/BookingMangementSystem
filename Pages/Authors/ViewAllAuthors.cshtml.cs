using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
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

        public List<Author> Authors { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch the list of authors from the backend API
            var response = await _httpClient.GetAsync("http://localhost:5097/api/Author");

            if (response.IsSuccessStatusCode)
            {
                Authors = await response.Content.ReadAsAsync<List<BookingMangementSystem.Models.Author>>();
            }
            else
            {
                Authors = new List<BookingMangementSystem.Models.Author>(); // Handle the error accordingly (e.g., log the issue)
            }
        }
    }
}