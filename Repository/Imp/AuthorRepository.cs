using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
using BookingMangementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingMangementSystem.Repository.Imp
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.Include(b => b.Books).ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.Include(b => b.Books).FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}