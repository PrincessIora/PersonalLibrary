using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Models;

namespace PersonalLibrary.Data
{
    public class LibraryContext : DbContext
    {

        public LibraryContext( 
            DbContextOptions<LibraryContext> options) 
            : base(options) 
        {
        }
            
        
    public DbSet<Book> Books => Set<Book>();


    public DbSet<ReadingHistory> ReadingHistories => Set<ReadingHistory>();

    }
}
