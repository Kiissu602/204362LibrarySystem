using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class AddMemberDTO
    {
        public IFormFile Image { get; set; }
        public string MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public int Faculty { get; set; }
        public int Department { get; set; }
        public int Job { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
