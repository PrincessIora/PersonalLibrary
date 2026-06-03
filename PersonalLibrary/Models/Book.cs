using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? Description { get; set; }

        public BookStatus Status { get; set; }
        public double? Rating {  get; set; }
        public int? Year {  get; set; }


    }
}
