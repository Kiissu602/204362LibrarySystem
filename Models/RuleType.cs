using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class RuleType
    {
        
        public int RuleID { get; set; }

        public Rule Rule { get; set; }

        public int JobID { get; set; }

        public Job Job { get; set; }
    }
}
