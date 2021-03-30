using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _204362LibrarySystem.Models;
using _204362LibrarySystem.Models.DTO;
using LibrarySystem.Services;
using _204362LibrarySystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace _204362LibrarySystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly BookService _bookService;
        private readonly WriterService _writerService;
        private readonly PublisherService _publisherService;
        private readonly AuthorService _authorService;

        public BooksController(DatabaseContext context, BookService bookService, WriterService writerService, PublisherService publisherService, AuthorService authorService)
        {
            _context = context;
            _bookService = bookService;
            _writerService = writerService;
            _publisherService = publisherService;
            _authorService = authorService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            return await _context.Book.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<BookFormDTO> GetBook(string id)
        {
            var book = await _context.Book.Select(b => new BookFormDTO
            {
                Image = b.BookImgUrl,
                ISBN = b.ISBN,
                Title = b.Title,
                PublisherName = b.Publisher.PublisherName,
                PublicationDate = b.PublicationDate,
                Category = b.Category.CategoryName,
                Edition = b.Edition,
                Pagination = b.Pagination,
                Price = b.Price,
                Plot = b.Plot,
                Writer = b.Authorlist.Select(a => a.Writer.WriterName).ToList(),
            }).FirstOrDefaultAsync(b => b.ISBN == id);

            if (book == null)
            {
                return null;
            }

            return book;
        }
        [HttpGet("up/{id}")]
        public async Task<BookFormUpdateDTO> GetBookToUpdate(string id)
        {
            var book = await _context.Book.Select(b => new BookFormUpdateDTO
            {
                Image = b.BookImgUrl,
                ISBN = b.ISBN,
                Title = b.Title,
                PublisherID = b.Publisher.PublisherID,
                PublisherName = b.Publisher.PublisherName,
                PublicationDate = b.PublicationDate,
                Category = b.Category.CategoryID,
                Edition = b.Edition,
                Pagination = b.Pagination,
                Price = b.Price,
                Plot = b.Plot,
                WriterID = b.Authorlist.Select(a => a.Writer.WriterID).ToList(),
                WriterName = b.Authorlist.Select(a => a.Writer.WriterName).ToList(),
            }).FirstOrDefaultAsync(b => b.ISBN == id);

            if (book == null)
            {
                return null;
            }

            return book;
        }

        [HttpGet("bydata")]
        public async Task<List<BookFormDTO>> GetBookBydata([FromQuery]string ISBN, [FromQuery] string Writer, [FromQuery] string Publisher, [FromQuery] string Title)
        {
            var newBook = await _context.Book.Where(b => (ISBN == null || b.ISBN.Contains(ISBN))
            && (Title == null || b.Title.Contains(Title))
            && (Publisher == null || b.Publisher.PublisherName.Contains(Publisher))
            && (Writer == null || b.Authorlist.Any(a => a.Writer.WriterName.Contains(Writer)))).Select(b => new BookFormDTO
            {
                Image = b.BookImgUrl,
                ISBN = b.ISBN,
                Title = b.Title,
                PublisherName = b.Publisher.PublisherName,
                PublicationDate = b.PublicationDate,
                Category = b.Category.CategoryName,
                Edition = b.Edition,
                Pagination = b.Pagination,
                Price = b.Price,
                Plot = b.Plot,
                Writer = b.Authorlist.Select(a => a.Writer.WriterName).ToList(),
            }).ToListAsync();

            if (newBook == null)
            {
                return null;
            }

            return newBook;
        }
        // PUT: api/Books/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromForm]UpdateBookDTO book)
        {
                
            var publisher = _publisherService.Post(book.PublisherName);

            if (publisher != null)
            {
                _context.Publisher.Add(publisher);
            }
            await _context.SaveChangesAsync();
            Book newBook = _bookService.Put(book);

            var entry = _context.Entry(newBook);
            entry.State = EntityState.Modified;
            entry.Property(b => b.BookImgUrl).IsModified = book.Image != null;
            entry.Reference(b => b.Publisher).IsModified = publisher != null;

            string[] writers = book.WriterName.Split(',');

            for (int i = 0; i < writers.Length; i++)
            {
                if (i >= writers.Length)
                {
                    var writer = new Writer { WriterName = writers[i].Trim() };
                    _context.Writer.Add(writer);
                    var newAuthor = new Author { Book = newBook, Writer = writer };
                    _context.Author.Add(newAuthor);
                }
                else if (_context.Writer.Any(e => e.WriterName != writers[i].Trim() && e.WriterID == book.WriterID.ElementAt(i)))
                {
                    Writer writer = new Writer { WriterID = book.WriterID.ElementAt(i), WriterName = writers[i] };
                    _context.Entry(writer).State = EntityState.Modified;
                }
                
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromForm]AddBookDTO book)
        {
            if (BookExists(book.ISBN))
            {
                return BadRequest(new { msg = "DuplicateISBN" });
            }
            var newWriter = _writerService.Post(book.WriterName);
            foreach(var w in newWriter)
            {
                _context.Writer.Add(w);
            }

            var publisher = _publisherService.Post(book.PublisherName);
            if(publisher != null)
            {
                _context.Publisher.Add(publisher);
            }

            var newBook = _bookService.Post(book);
            _context.Book.Add(newBook);

            var newAuthor = _authorService.Post(newBook, book.WriterName);
            foreach(var a in newAuthor)
            {
                _context.Author.Add(a);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.ISBN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBook", new { id = book.ISBN }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(string id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(string id)
        {
            return _context.Book.Any(e => e.ISBN == id);
        }

    }
}
