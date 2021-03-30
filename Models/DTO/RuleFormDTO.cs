using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class RuleFormDTO
    {
        public int JobId { get; set; }
        public int Amount { get; set; }
        public int LimitDayBorrow { get; set; }
        public int LimitDayBooking { get; set; }
        public int ReturnFines { get; set; }
        public int LostFines { get; set; }
        public int GetBook { get; set; }
    }
}
