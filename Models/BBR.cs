using _204362LibrarySystem.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class BBR
    {
        [Key]
        public int BorrowID { get; set; }

        [Required]
        [Column(TypeName = "char(9)")]
        public string MemberID { get; set; }

        [Required]
        public Member Member { get; set; }

        [Required]
        [Column(TypeName = "char(13)")]
        public string ISBN { get; set; }

        [Required]
        public Book Book { get; set; }

        
        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime BookingDate { get; set; }

        [Required]
        [Column(TypeName = "char(10)")]
        public string PhoneTemp { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string ReservePlace { get; set; }

        public EnumType Status { get; set; } 
    }
}
