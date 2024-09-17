using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingMangementSystem.Pages.Books
{
    [Authorize(Roles = "admin")]
    public class Create : PageModel
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpClientFactory _clientFactory;

        public Create(IAuthorRepository authorRepository, IWebHostEnvironment webHostEnvironment, IHttpClientFactory clientFactory)
        {
            _authorRepository = authorRepository;
            _webHostEnvironment = webHostEnvironment;
            _clientFactory = clientFactory;
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

            string filePath = ProcessUploadedFile();  // Process the file from Input.UploadedFile

            // Set up the HTTP client
            var client = _clientFactory.CreateClient();

            using var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(Input.Id.ToString()), "id");
            formData.Add(new StringContent(Input.Name), "name");
            formData.Add(new StringContent(Input.Description), "description");
            formData.Add(new StringContent(Input.Summary), "summary");
            formData.Add(new StringContent(Input.AuthorId.ToString()), "authorId");
            formData.Add(new StringContent(Input.IsRecommended.ToString()), "isRecommended");

            // Attach the file
            if (Input.UploadedFile != null)
            {
                var fileContent = new StreamContent(Input.UploadedFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(Input.UploadedFile.ContentType);
                formData.Add(fileContent, "filePath", Input.UploadedFile.FileName);
            }

            var response = await client.PostAsync("http://localhost:5284/api/Book", formData);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = $"Successfully added book '{Input.Name}'.";
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "There was an error adding the book.");
                return Page();
            }
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