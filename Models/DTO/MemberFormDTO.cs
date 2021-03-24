using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class MemberFormDTO
    {
        public string MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string  FacultyName { get; set; }
        public string DepartmentName { get; set; }
        public string Email { get; set; }
    }
}
