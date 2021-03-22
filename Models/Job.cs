using _204362LibrarySystem.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }

        [Column(TypeName = "char(10)")]
        public EnumType JobName { get; set; }

        public ICollection<Member> MemberList { get; set; }
    }
}
