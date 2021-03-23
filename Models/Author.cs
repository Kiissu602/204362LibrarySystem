using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class Author
    {
        [Column(TypeName = "char(13)")]
        public string ISBN { get; set; }

        public Book Book { get; set; }

        public int WriterID { get; set; }

        public Writer Writer { get; set; }
    }
}
