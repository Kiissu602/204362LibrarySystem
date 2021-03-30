using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _204362LibrarySystem.Models;
using _204362LibrarySystem.Models.DTO;
using _204362LibrarySystem.Enum;

namespace _204362LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BBRsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BBRsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BBRs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BBR>>> GetBBR()
        {
            return await _context.BBR.ToListAsync();
        }

        // GET: api/BBRs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BBR>> GetBBR(int id)
        {
            var bBR = await _context.BBR.FindAsync(id);

            if (bBR == null)
            {
                return NotFound();
            }

            return bBR;
        }
        [HttpGet("member")]
        public async Task<List<BorrowListDTO>> GetBBRByMemberID(ReturnMemDTO member)
        {
           
            List<BorrowListDTO> bBR = await _context.BBR.Where(b => b.MemberID.Contains(member.MemberID)).Select(b => new BorrowListDTO { 
                BorrowDate = b.BookingDate,
                DueDate = (b.ReservePlace =="Library" ? b.BorrowDate.AddDays(b.Member.Job.Rule.LimitDayBorrow): b.BorrowDate.AddDays(b.Member.Job.Rule.LimitDayBooking)),
                FirstName = b.Member.FirstName,
                LastName = b.Member.LastName,
                BookName = b.Book.Title,
            }).ToListAsync();

            return bBR;
        }
        

        // PUT: api/BBRs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBBR(int id, BBR bBR)
        {
            if (id != bBR.BorrowID)
            {
                return BadRequest();
            }

            _context.Entry(bBR).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BBRExists(id))
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

        // POST: api/BBRs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BorrowListDTO>> PostBBR(AddBorrowDTO bbr)
        {
            Member member = await _context.Member.Include(m => m.Job.Rule).FirstOrDefaultAsync(m => m.MemberID == bbr.MemberID);
            
            Book book = await _context.Book.FirstOrDefaultAsync(b => b.ISBN == bbr.ISBN);
            
            
            var count = _context.BBR.Count(b => b.MemberID == member.MemberID && (b.Status == EnumType.Borrowing || b.Status == EnumType.Booking));
            
            if (member.Job.Rule.Amount > count)
            {
                BBR newBorrow = new BBR
                {
                    MemberID = member.MemberID,
                    ISBN = book.ISBN,
                    BorrowDate = DateTime.Now,
                    PhoneTemp = member.Phone,
                    ReservePlace = "Library",
                    Status = EnumType.Borrowing,
                };
                _context.BBR.Add(newBorrow);
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            return BadRequest(new { msg = "BookLimited" });
        }

        // DELETE: api/BBRs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BBR>> DeleteBBR(int id)
        {
            var bBR = await _context.BBR.FindAsync(id);
            if (bBR == null)
            {
                return NotFound();
            }

            _context.BBR.Remove(bBR);
            await _context.SaveChangesAsync();

            return bBR;
        }

        private bool BBRExists(int id)
        {
            return _context.BBR.Any(e => e.BorrowID == id);
        }
    }
}
