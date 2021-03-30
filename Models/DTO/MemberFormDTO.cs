using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class MemberFormDTO
    {
        public string Image { get; set; }
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

    public class MemberListDTO
    {
        public string MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FacultyName { get; set; }
        public string DepartmentName { get; set; }

    }

    public class MemberRuleDTO
    {
        public string MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DueDate { get; set; }
        public int Amount { get; set; }
    }
}
