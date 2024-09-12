using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
using BookingMangementSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingMangementSystem.Pages.Books
{
    [Authorize(Roles = "admin")]
    public class Create : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Create(IBookRepository bookRepository, IAuthorRepository authorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public BookInputModel Input { get; set; }

        public IEnumerable<Author> Authors { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Authors = await _authorRepository.GetAllAuthorsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Authors = await _authorRepository.GetAllAuthorsAsync();
                return Page();
            }

            // Log to check if file is received
            if (Input.UploadedFile == null)
            {
                Console.WriteLine("File is null.");
            }
            else
            {
                Console.WriteLine($"File uploaded: {Input.UploadedFile.FileName}");
            }

            string filePath = ProcessUploadedFile();  // Process the file from Input.UploadedFile

            var book = new Book
            {

                AuthorId = Input.AuthorId,
                Name = Input.Name,
                FilePath = filePath,
                IsRecommended = Input.IsRecommended,
                Description = Input.Description,
                Summary = Input.Summary,
            };

            await _bookRepository.AddBookAsync(book);

            // Store success message in TempData
            TempData["SuccessMessage"] = $"Successfully added book '{book.Name}'.";

            return RedirectToPage("/Index");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Input.UploadedFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + Input.UploadedFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Input.UploadedFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}