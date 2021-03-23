using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class Writer
    {
        [Key]
        public int WriterID { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string WriterName { get; set; }

        public ICollection<Author> Authorlist { get; set; }
    }
}
