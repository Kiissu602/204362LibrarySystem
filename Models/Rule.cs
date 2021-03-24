using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public class Rule
    {   
        [Key]
        public int RuleID { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int LimitDayBorrow { get; set; }

        [Required]
        public int LimitDayBooking { get; set; }

        [Required]
        public TimeSpan BookingTime { get; set;}

        [Required]
        public int ReturnFines { get; set; }

        [Required]
        public int LostFines { get; set; }

        public ICollection<Job> Job { get; set; }

    }
}
