using PersonalLibrary.Models;

namespace PersonalLibrary.Repositories
{
    public interface IBookRepository
    {
        Task Add(Book book);

        Task<List<Book>> GetAll();

        Task<Book> Get(int id);

        Task Save();
        Task AddHistory(ReadingHistory history);
        Task<List<ReadingHistory>> GetHistory(int bookId);
    }
}
