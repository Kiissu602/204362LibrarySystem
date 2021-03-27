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
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using _204362LibrarySystem.Services;

namespace _204362LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        
        private readonly DatabaseContext _context;
        private readonly IMemberSettings _memberSettings;
        private readonly MemberService _memberService;
        public MembersController(DatabaseContext context, IMemberSettings memberSettings,MemberService memberService)
        {
            _context = context;
            _memberSettings = memberSettings;
            _memberService = memberService;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            return await _context.Member.ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<MemberFormDTO> GetMember(string id)
        {
            var member = await _context.Member.Select(m => new MemberFormDTO 
                {
                MemberID = m.MemberID,
                FirstName = m.FirstName,
                LastName = m.LastName,
                BirthDate = m.BirthDate,
                Sex = m.Sex,
                Phone = m.Phone,
                FacultyName = m.Faculty.FacultyName,
                DepartmentName = m.Department.DepartmentName,
                Email = m.Email,
            }).FirstOrDefaultAsync(m=> m.MemberID == id);

            return member;
        }


        [HttpPut]
        public async Task<ActionResult<Member>> PutMember([FromForm] AddMemberDTO member)
        {
            var umember = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (umember == null)
            {
                return Unauthorized();
            }

            Member Member = _memberService.Put(member);
            var entry = _context.Entry(Member);
            entry.State = EntityState.Modified;
            entry.Property(m => m.ImgUrl).IsModified = member.Image != null;
            entry.Property(m => m.Password).IsModified = member.Password != null;

            await _context.SaveChangesAsync();
            return NoContent();
        }


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

        [HttpPost]
        public async Task<ActionResult<Member>> PostMember([FromForm] AddMemberDTO member)
        {

            Member newMember = _memberService.Post(member);
            _context.Member.Add(newMember);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemberExists(member.MemberID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetMember", new { id = member.MemberID }, newMember);
        }

        [HttpPost("login")]
        public ActionResult<MemberReturnDTO> Authenticate(LoginDTO login)
        {
            Member member = _context.Member.Include(m => m.Faculty).Include(m => m.Department).Include(m => m.Job).FirstOrDefault(member => member.Email == login.Email && member.Password == login.Password);

            // return null if user not found
            if (member == null)
            {
                return NotFound();
            }

            // authentication successful so generate jwt token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_memberSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", member.MemberID),
                    new Claim("Job", member.Job.JobName.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            MemberReturnDTO memberReturn = new MemberReturnDTO()
            {
                ImgUrl = member.ImgUrl,
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Sex = member.Sex,
                Phone = member.Phone,
                Faculty = member.Faculty,
                Department = member.Department,
                Job = member.Job,
                Email = member.Email,
                Token = tokenHandler.WriteToken(token)
            };
            return memberReturn;
        }

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
