using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Data;
using PersonalLibrary.Models;
using PersonalLibrary.Repositories;
using PersonalLibrary.Data;
using PersonalLibrary.Models;

namespace PersonalLibrary.Repositories
{
    public class BookRepository
    : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(
            LibraryContext context)
        {
            _context = context;
        }

        public async Task Add(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> Get(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddHistory(ReadingHistory history)
        {
            await _context.ReadingHistories.AddAsync(history);
        }

        public async Task<List<ReadingHistory>> GetHistory(int bookId)
        {
            return await _context.ReadingHistories
                .Where(h => h.BookId == bookId)
                .ToListAsync();
        }
    }
}