using _204362LibrarySystem.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class Type
    {
        [Key]
        public int TypeID { get; set; }

        [Column(TypeName = "char(10)")]
        public EnumType TypeName { get; set; }

        public ICollection<Member> MemberList { get; set; }
    }
}
