using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingMangementSystem.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}