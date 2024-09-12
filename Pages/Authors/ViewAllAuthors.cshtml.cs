using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
using BookingMangementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookingMangementSystem.Pages.Authors
{
    public class ViewAllAuthors : PageModel
    {
        private readonly IAuthorRepository _authorRepository;

        public ViewAllAuthors(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IEnumerable<Author> Authors { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                Authors = await _authorRepository.GetAllAuthorsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving authors." + ex.Message);
            }
        }
    }
}