using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _204362LibrarySystem.Models;
using LibrarySystem.Services;
using LibrarySystem.Setting;
using _204362LibrarySystem.Models.DTO;

namespace _204362LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ImageService _imageService;
        private readonly IMemberSettings _memberSettings;
        public MembersController(DatabaseContext context, IMemberSettings memberSettings, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
            _memberSettings = memberSettings;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await _context.Member.ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var member = await _context.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(string id, Member member)
        {
            if (id != member.MemberID)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Members
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember([FromForm] AddMemberDTO member)
        {
            var dep = new Department() { DepartmentID = member.Department };
            _context.Attach(dep);
            var fac = new Faculty() { FacultyID = member.Faculty };
            _context.Attach(fac);
            var type = new Models.Type() { TypeID = member.Type };
            _context.Attach(type);
            string Img = _imageService.SaveImg(member.Img);

            var newMember = new Member()
            {
                ImgUrl = Img,
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Sex = member.Sex,
                Phone = member.Phone,
                Faculty = fac,
                Department = dep,
                Type = type,
                Email = member.Email,
                Password = member.Password,
  
            };

            _context.Member.Add(newMember);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = member.MemberID }, newMember);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Member>> DeleteMember(string id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Member.Remove(member);
            await _context.SaveChangesAsync();

            return member;
        }

        private bool MemberExists(string id)
        {
            return _context.Member.Any(e => e.MemberID == id);
        }
    }
}
