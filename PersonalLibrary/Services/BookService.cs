using Microsoft.EntityFrameworkCore;
using PersonalLibrary.DTOs;
using PersonalLibrary.Models;
using PersonalLibrary.Models;
using PersonalLibrary.Repositories;

namespace PersonalLibrary.Services
{
    public class BookService
    {
        private readonly IBookRepository _repo;

        public BookService(
            IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task AddBook(Book book)
        {
            await _repo.Add(book);

            await _repo.Save();
        }

        public async Task UpdateBook(
            int id,
            UpdateBookDTO dto)
        {
            var book = await _repo.Get(id);

            if (book == null)
                throw new Exception("Book not found");


            if (dto.Status.HasValue && dto.Status != book.Status)
            {
                var allBooks = await _repo.GetAll();

                int readingCount =
            allBooks.Count(x => x.Status == BookStatus.Reading);

                // enforce rule
                if (dto.Status == BookStatus.Reading &&
                    book.Status != BookStatus.Reading &&
                    readingCount >= 3)
                {
                    throw new Exception("Max 3 books in Reading state");
                }

                // state transition rule
                if (!IsValidTransition(book.Status, dto.Status.Value))
                {
                    throw new Exception(
                        $"Invalid transition: {book.Status} → {dto.Status}");
                }

                // apply valid transition
                book.Status = dto.Status.Value;
            }

            if (dto.Description != null)
                book.Description = dto.Description;

            if (dto.Rating.HasValue)
                book.Rating = dto.Rating;

            if (dto.Year.HasValue)
            {
                if(dto.Rating < 1 || dto.Rating > 5)
                {
                    throw new Exception("Rating must be between 1.0 and 5.0");
                }
                book.Year = dto.Year;
            }

            await _repo.AddHistory(new ReadingHistory
            {
                BookId = book.Id,
                Date = DateTime.UtcNow,
                Status = book.Status
            });



            await _repo.Save();
        }



        public async Task<List<Book>> Search(
    string? title,
    BookStatus? status)
        {
            var books = await _repo.GetAll();

            if (!string.IsNullOrWhiteSpace(title))
            {
                books = books
                    .Where(b =>
                        b.Title.ToLower()
                        .Contains(title.ToLower()))
                    .ToList();
            }

            if (status.HasValue)
            {
                books = books
                    .Where(b => b.Status == status)
                    .ToList();
            }

            return books;
        }


        public async Task<bool> HasActiveStreak(int bookId, DateTime currentDate)
        {
            var history = await _repo.GetHistory(bookId);

            var lastFinish = history
                .Where(h => h.Status == BookStatus.Finished)
                .OrderByDescending(h => h.Date)
                .FirstOrDefault();

            if (lastFinish == null)
                return false;

            var daysDiff =
                (currentDate.Date - lastFinish.Date.Date).TotalDays;

            return daysDiff <= 1;
        }

        public async Task<List<ReadingHistory>> GetHistory(int bookId)
        {
            return await _repo.GetHistory(bookId);
        }

        private bool IsValidTransition(BookStatus from, BookStatus to)
        {
            return from switch
            {
                BookStatus.WantToRead =>
                    to == BookStatus.Reading,

                BookStatus.Reading =>
                    to == BookStatus.Finished,

                BookStatus.Finished =>
                    to == BookStatus.Reading,

                _ => false
            };
        }

    }
}