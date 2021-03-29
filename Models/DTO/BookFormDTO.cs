using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class BookFormDTO
    {
        public string Image { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string PublisherName { get; set; }
        public string PublicationDate { get; set; }
        public int Edition { get; set; }
        public int Pagination { get; set; }
        public float Price { get; set; }
        public string Plot { get; set; }
        public string Category { get; set; }
        public List<string> Writer { get; set; }
    }

    public class BookFormUpdateDTO
    {
        public string Image { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string PublisherName { get; set; }
        public int PublisherID { get; set; }
        public string PublicationDate { get; set; }
        public int Edition { get; set; }
        public int Pagination { get; set; }
        public float Price { get; set; }
        public string Plot { get; set; }
        public int Category { get; set; }
        public List<int> WriterID { get; set; }
        public List<string> WriterName { get; set; }
        
    }
}
