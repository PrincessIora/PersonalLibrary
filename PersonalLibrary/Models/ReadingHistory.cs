namespace PersonalLibrary.Models
{
    public class ReadingHistory
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public DateTime Date { get; set; }

        public BookStatus Status { get; set; }
    }
}
