using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class AddBookDTO
    {
        public IFormFile Image { get; set; }
        public string ISBN { get; set; }
        public int PublisherID { get; set; }
        public int PublicationDate { get; set; }
        public int Edition { get; set; }
        public int Pagination { get; set; }
        public float Price { get; set; } 
        public string Plot { get; set; }
        public string CategoryiD { get; set; }
    }
}
