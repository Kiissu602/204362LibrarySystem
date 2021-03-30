using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class SearchBookDTO
    {
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string Writer { get; set; }
        public string Title { get; set; }
    }
}
