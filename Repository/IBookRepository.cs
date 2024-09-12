
using BookingMangementSystem.Models;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BookingMangementSystem.Repository
{
    [ScopedService]
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm, bool? isRecommended, bool? hasFile);
    }
}