using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BooksSystem.Pages.Account
{
    public class Logout : PageModel
    {
        private readonly HttpClient _httpClient;

        public Logout(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClient.PostAsync("https://your-backend-api.com/api/auth/logout", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "There was an error logging out.");
            return Page();
        }
    }
}