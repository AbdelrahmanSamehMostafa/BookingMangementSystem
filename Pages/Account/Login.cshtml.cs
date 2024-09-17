using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BooksSystem.Pages.Account
{
    public class Login : PageModel
    {
        private readonly HttpClient _httpClient;

        public Login(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public class LoginInputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _httpClient.PostAsJsonAsync("https://your-backend-api.com/api/auth/login", Input);

            if (response.IsSuccessStatusCode)
            {
                // Handle successful login, e.g., store the token in a cookie or session
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}