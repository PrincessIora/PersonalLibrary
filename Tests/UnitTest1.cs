using FluentAssertions;
using PersonalLibrary.DTOs;
using PersonalLibrary.Models;
using PersonalLibrary.Services;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task AddBook_ShouldStoreBook()
        {
            var repo = new FakeBookRepository();
            var service = new BookService(repo);

            await service.AddBook(new Book
            {
                Title = "Test",
                Author = "Author"
            });

            repo.Books.Should().HaveCount(1);
        }

        [Fact]
        public async Task Search_ShouldFilterByTitle()
        {
            var repo = new FakeBookRepository();
            var service = new BookService(repo);

            await service.AddBook(new Book
            {
                Title = "Harry Potter",
                Author = "JK",
                Status = BookStatus.Finished
            });

            var result = await service.Search("harry", null);

            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task WantToRead_To_Reading_ShouldBeValid()
        {
            var repo = new FakeBookRepository();
            var service = new BookService(repo);

            await service.AddBook(new Book
            {
                Title = "Book",
                Author = "A",
                Status = BookStatus.WantToRead
            });

            await service.UpdateBook(1, new UpdateBookDTO
            {
                Status = BookStatus.Reading
            });

            var book = await repo.Get(1);

            book.Status.Should().Be(BookStatus.Reading);
        }

        [Fact]
        public async Task WantToRead_To_Finished_ShouldFail()
        {
            var repo = new FakeBookRepository();
            var service = new BookService(repo);

            await service.AddBook(new Book
            {
                Status = BookStatus.WantToRead
            });

            Func<Task> act = async () =>
                await service.UpdateBook(1, new UpdateBookDTO
                {
                    Status = BookStatus.Finished
                });

            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Should_Not_Allow_More_Than_3_Reading()
        {
            var repo = new FakeBookRepository();
            var service = new BookService(repo);

            for (int i = 0; i < 3; i++)
            {
                await service.AddBook(new Book
                {
                    Status = BookStatus.Reading
                });
            }

            await service.AddBook(new Book
            {
                Status = BookStatus.WantToRead
            });

            Func<Task> act = async () =>
                await service.UpdateBook(4, new UpdateBookDTO
                {
                    Status = BookStatus.Reading
                });

            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Update_Should_Create_History_Record()
        {
            var repo = new FakeBookRepository();
            var service = new BookService(repo);

            await service.AddBook(new Book
            {
                Status = BookStatus.WantToRead
            });

            await service.UpdateBook(1, new UpdateBookDTO
            {
                Status = BookStatus.Reading
            });

            repo.History.Should().HaveCount(1);
        }

        [Fact]
        public async Task Streak_Should_Be_Active_Within_One_Day()
        {
            var repo = new FakeBookRepository();
            var service = new BookService(repo);

            await repo.AddHistory(new ReadingHistory
            {
                BookId = 1,
                Status = BookStatus.Finished,
                Date = DateTime.UtcNow.AddDays(-1)
            });

            var result =
                await service.HasActiveStreak(1, DateTime.UtcNow);

            result.Should().BeTrue();
        }



    }
}