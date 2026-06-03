using PersonalLibrary.Models;
using PersonalLibrary.Repositories;

public class FakeBookRepository : IBookRepository
{
    public List<Book> Books = new();
    public List<ReadingHistory> History = new();

    public Task Add(Book book)
    {
        book.Id = Books.Count + 1;
        Books.Add(book);
        return Task.CompletedTask;
    }

    public Task<List<Book>> GetAll()
        => Task.FromResult(Books);

    public Task<Book?> Get(int id)
        => Task.FromResult(Books.FirstOrDefault(b => b.Id == id));

    public Task Save()
        => Task.CompletedTask;

    public Task AddHistory(ReadingHistory h)
    {
        History.Add(h);
        return Task.CompletedTask;
    }

    public Task<List<ReadingHistory>> GetHistory(int bookId)
    {
        return Task.FromResult(
            History.Where(h => h.BookId == bookId).ToList());
    }
}