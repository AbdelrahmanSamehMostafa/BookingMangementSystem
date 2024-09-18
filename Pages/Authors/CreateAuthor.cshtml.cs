using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
using BookingMangementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingMangementSystem.Pages.Authors
{
    public class CreateAuthor : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateAuthor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public Author Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ensure that the Books property is not null
            if (Input.Books == null)
            {
                Input.Books = new List<Book>();
            }

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5284/api/Author", Input);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "There was an error during registration.");
            return Page();
        }
    }
}