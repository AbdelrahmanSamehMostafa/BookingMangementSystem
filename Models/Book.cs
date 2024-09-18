using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingMangementSystem.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public bool IsRecommended { get; set; }
    }
}