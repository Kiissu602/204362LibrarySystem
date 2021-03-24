using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherID { get; set; }

        public string PublisherName { get; set; }
        
        public ICollection<Book> Booklist { get; set; }
    }
}
