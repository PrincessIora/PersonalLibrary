using PersonalLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.DTOs
{
    public class UpdateBookDTO
    {
        public BookStatus? Status { get; set; }
        public string? Description { get; set; }
        public double? Rating { get; set; }
        public int? Year { get; set; }
    }
}
