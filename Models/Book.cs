using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class Book
    {
        public string BookImgUrl { get; set; }

        [Key]
        [Column(TypeName = "char(13)")]
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public Category Category { get; set; }

        [Required]
        public Publisher Publisher { get; set; }

        public string PublicationDate { get; set; }

        [Required]
        public int Edition { get; set; }

        [Required]
        public int Pagination { get; set; }

        [Required]
        public float Price { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Plot { get; set; }

        public ICollection<Author> Authorlist { get; set; }
    }
}
