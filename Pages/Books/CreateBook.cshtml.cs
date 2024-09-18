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
    public class CreateBook : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateBook(HttpClient httpClient, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClient;
            _webHostEnvironment = webHostEnvironment;
        }

        // Book Input Model to handle form data
        [BindProperty]
        public Book Input { get; set; }

        // List of Authors to populate the dropdown
        public List<Author> Authors { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch the list of authors from the backend API
            var response = await _httpClient.GetAsync("http://localhost:5284/api/Author");

            if (response.IsSuccessStatusCode)
            {
                Authors = await response.Content.ReadAsAsync<List<Author>>();
            }
            else
            {
                Authors = new List<Author>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // string filePath = ProcessUploadedFile();

            // Validate the model
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Prepare the book data to send to the API
            var bookToAdd = new Book
            {
                Id = Guid.Empty,
                Name = Input.Name,
                Description = Input.Description,
                Summary = Input.Summary,
                IsRecommended = Input.IsRecommended,
                AuthorId = Input.AuthorId,
                // FilePath = filePath
            };

            // Make the POST request to add the book to the database
            var response = await _httpClient.PostAsJsonAsync("http://127.0.0.1:5284/api/Book", bookToAdd);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Books/ViewAllBooks");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "There was an error adding the book.");
                return Page();
            }
        }

        // private string ProcessUploadedFile()
        // {
        //     string uniqueFileName = null;

        //     if (Input.UploadedFile != null)
        //     {
        //         string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        //         if (!Directory.Exists(uploadsFolder))
        //         {
        //             Directory.CreateDirectory(uploadsFolder);
        //         }

        //         uniqueFileName = Guid.NewGuid().ToString() + "_" + Input.UploadedFile.FileName;
        //         string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //         using (var fileStream = new FileStream(filePath, FileMode.Create))
        //         {
        //             Input.UploadedFile.CopyTo(fileStream);
        //         }
        //     }

        //     return uniqueFileName;
        // }
    }
}