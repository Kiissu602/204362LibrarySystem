using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }

        public string FacultyName { get; set; }

        public ICollection<Department> Departmentlist { get; set; }
    }
}
