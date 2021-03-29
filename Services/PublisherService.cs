using _204362LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Services
{
    public class PublisherService
    {
        private readonly DatabaseContext _context;
        public PublisherService(DatabaseContext context)
        {
            _context = context;
        }

        public Publisher Post(string publisher)
        {
            if (PublisherExistsByName(publisher))
            {
                return null;
            }
            Publisher newPublisher = new Publisher { PublisherName = publisher };
            return newPublisher;
        }

        public Publisher Put(string publisher)
        {
            if (PublisherExistsByName(publisher))
            {
                return null;
            }
            Publisher newPublisher = new Publisher { PublisherName = publisher };
            return newPublisher;
        }

        private bool PublisherExistsByName(string publisher)
        {
            return _context.Publisher.Any(e => e.PublisherName == publisher);
        }
    }

}
