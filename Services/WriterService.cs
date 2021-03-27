using _204362LibrarySystem.Models;
using _204362LibrarySystem.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Services
{
    public class WriterService
    {
        private readonly DatabaseContext _context;

        public WriterService(DatabaseContext context)
        {
            _context = context;
        }

        public List<Writer> Post(string writer)
        {
            string[] writers = writer.Split(',');
            List<Writer> newWriters = new List<Writer>();
            foreach(var w in writers)
            {
                if(!WriterExistsByName(w))
                {
                    var name = new Writer { WriterName = w };
                    newWriters.Add(name);
                }
            }
            return newWriters;
        }

        private bool WriterExistsByName(string name)
        {
            return _context.Writer.Any(e => e.WriterName == name);
        }
    }
}
