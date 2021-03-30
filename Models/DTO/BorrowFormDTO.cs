using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class BorrowListDTO
    {
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BookName { get; set; }
    }

    public class ReturnListDTO
    {
        public string ISBN { get; set; }
        public string MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BookName { get; set; }
    }
    public class ReturnMemDTO
    {
        public string MemberID { get; set; }
    }

}
