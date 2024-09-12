using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingMangementSystem.Models
{
    public class BookInputModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }

        public bool IsRecommended { get; set; }

        public string? FilePath { get; set; }
        
        public IFormFile? UploadedFile { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}