using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingMangementSystem.Pages.Books
{
    public class CreateBook : PageModel
    {
        // Properties for Book data
        [BindProperty]
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        // Logger (optional, if you need to log something)
        private readonly ILogger<CreateBook> _logger;

        public CreateBook(ILogger<CreateBook> logger)
        {
            _logger = logger;
        }

        // This method is called when the form is posted
        public IActionResult OnPost()
        {
            // Check if the model is valid (validation is performed here)
            if (!ModelState.IsValid)
            {
                // If validation fails, return to the page with validation messages
                return Page();
            }

            // If everything is valid, you can proceed with further actions, like saving the data
            // You can add logic to save the book to a database or some storage

            // Example of logging the creation (optional)
            _logger.LogInformation($"Book created with Title: {Title}");

            // Redirect back to the author creation page or another page
            return RedirectToPage("/Authors/CreateAuthor"); // Adjust the page name as needed
        }
    }
}
