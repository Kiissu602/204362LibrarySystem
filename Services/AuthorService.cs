using _204362LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Services
{
    public class AuthorService
    {
        private readonly DatabaseContext _context;
        public AuthorService(DatabaseContext context)
        {
            _context = context;
        }

        public List<Author> Post(Book book, string WriterName)
        {
            string[] writers = WriterName.Split(',');
            List<Author> newAuthor = new List<Author>();
            foreach (var w in writers)
            {
                var writer = new Writer() { WriterName = w.Trim() };
                _context.Attach(writer);
                newAuthor.Add(new Author { Book = book, Writer = writer });
            }
            return newAuthor;
        }
    }
}
