using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingMangementSystem.Models;
using BookingMangementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingMangementSystem.Repository.Imp
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            // Check if the author exists
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == book.AuthorId);

            if (author == null)
            {
                throw new ArgumentException("Author not found.");
            }

            // Add the book to the context
            _context.Books.Add(book);

            // Add the book to the author's book list
            author.Books.Add(book);

            // Save changes to the context
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, bool? isRecommended, bool? hasFile)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Name.Contains(searchTerm));
            }

            if (isRecommended.HasValue)
            {
                query = query.Where(b => b.IsRecommended == isRecommended.Value);
            }

            if (hasFile.HasValue)
            {
                if (hasFile.Value)
                {
                    query = query.Where(b => !string.IsNullOrEmpty(b.FilePath));
                }
                else
                {
                    query = query.Where(b => string.IsNullOrEmpty(b.FilePath));
                }
            }

            return await query.ToListAsync();
        }
    }
}