using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _204362LibrarySystem.Models;
using _204362LibrarySystem.Models.DTO;

namespace _204362LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AuthorsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthor()
        {
            return await _context.Author.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(string id)
        {
            var author = await _context.Author.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(string id, Author author)
        {
            if (id != author.ISBN)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AddAuthorDTO author)
        {
            string[] writers = author.WriterName.Split(',');
            var book = new Book() { ISBN = author.ISBN };
            _context.Attach(book);
            foreach (var w in writers)
            {
                var writer = new Writer() { WriterName = w.Trim() };
                _context.Attach(writer);
                var newAuthor = new Author { Book = book, Writer = writer };
                _context.Author.Add(newAuthor);
            }
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(string id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return author;
        }

        private bool AuthorExists(string id)
        {
            return _context.Author.Any(e => e.ISBN == id);
        }
    }
}
