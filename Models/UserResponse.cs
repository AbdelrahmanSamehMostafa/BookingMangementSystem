using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingMangementSystem.Models
{
    public class UserResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
    }
}