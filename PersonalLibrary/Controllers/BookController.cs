using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.DTOs;
using PersonalLibrary.Models;
using PersonalLibrary.Services;

namespace PersonalLibrary.Controllers
{

    [ApiController]

    [Route("books")]

    public class BooksController
    : ControllerBase
    {
        private readonly BookService _service;

        public BooksController(
            BookService service)
        {
            _service = service;
        }

        [HttpPost]

        public async Task<IActionResult>
        Create(CreateBookDTO dto)
        {
            await _service.AddBook(
                new Book
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Status =
                        BookStatus
                        .WantToRead
                });

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(
    int id,
    [FromBody] UpdateBookDTO dto)
        {
            await _service.UpdateBook(id, dto);
            return Ok();
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(
    [FromQuery] string? title,
    [FromQuery] BookStatus? status)
        {
            var results = await _service.Search(title, status);
            return Ok(results);
        }



        [HttpGet("{id}/streak")]
        public async Task<IActionResult> GetStreak(
    int id,
    [FromQuery] DateTime date)
        {
            var active =
                await _service.HasActiveStreak(id, date);

            return Ok(new
            {
                BookId = id,
                ActiveStreak = active
            });
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var history = await _service.GetHistory(id);
            return Ok(history);
        }




    }
}
