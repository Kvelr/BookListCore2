using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookList.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
