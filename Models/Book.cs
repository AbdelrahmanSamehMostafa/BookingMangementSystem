using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingMangementSystem.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public string Summary { get; set; }

        public bool IsRecommended { get; set; }

        [Required(ErrorMessage = "Please select an author.")]
        public string AuthorId { get; set; }

        // public IFormFile? UploadedFile { get; set; }

        // public string? FilePath { get; set; }
    }
}