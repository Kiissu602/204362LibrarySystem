using _204362LibrarySystem.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{

    public class Member
    {
        public string ImgUrl {get; set;}

        [Key]
        [Column(TypeName = "char(9)")]
        public string MemberID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [Column(TypeName = "nchar(10)")]
        public string Sex { get; set; }

        [Required]
        [Column(TypeName = "char(10)")]
        public string Phone { get; set; }

        [Required]
        public Faculty Faculty { get; set; }

        [Required]
        public Department Department { get; set; }

        [Required]
        public Job Job { get; set; }

        [Required]
        [Column(TypeName = "varchar(896)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(64)")]
        public string Password { get; set; }

        public ICollection<BBR> BBRlist { get; set; }
    }

    public class MemberReturnDTO
    {
        public string ImgUrl { get; set; }
        public string MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public Faculty Faculty { get; set; }
        public Department Department { get; set; }
        public Job Job { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
