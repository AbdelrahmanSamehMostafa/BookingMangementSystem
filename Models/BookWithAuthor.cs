using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingMangementSystem.Models
{
    public class BookWithAuthor
    {
        public Book Book { get; set; }
        public string AuthorName { get; set; }
    }
}