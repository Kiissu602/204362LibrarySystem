using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class UpdateBookDTO
    {
        public int Category { get; set; }
        public int Edition { get; set; }
        public IFormFile Image { get; set; }
        public string ISBN { get; set; }
        public int Pagination { get; set; }
        public string Plot { get; set; }
        public float Price { get; set; }
        public string PublicationDate { get; set; }
        public int PublisherID { get; set; }
        public string PublisherName { get; set; }
        public string Title { get; set; }
        public ICollection<int> WriterID { get; set; }
        public string WriterName { get; set; }
    }
}
