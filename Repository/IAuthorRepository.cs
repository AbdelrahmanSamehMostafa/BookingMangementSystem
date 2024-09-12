
using BookingMangementSystem.Models;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BookingMangementSystem.Repository
{
    [ScopedService]
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
    }
}