using _204362LibrarySystem.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class CheckMember
    {
        [Key]
        public string CheckMemberID { get; set; }
        [Column(TypeName = "char(9)")]
        public string MemberID { get; set; }
        public Member Member { get; set; }
        public EnumType CheckStatus { get; set; }
    }
}
