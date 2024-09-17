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
    public class Register : PageModel
    {
        private readonly HttpClient _httpClient;

        public Register(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public class RegisterInputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _httpClient.PostAsJsonAsync("https://your-backend-api.com/api/auth/register", Input);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "There was an error during registration.");
            return Page();
        }
    }
}