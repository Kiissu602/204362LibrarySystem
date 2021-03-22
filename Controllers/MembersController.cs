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

namespace _204362LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private const bool V = true;
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


        [HttpPut]
        public async Task<ActionResult<Member>> PutMember([FromForm] AddMemberDTO member)
        {
            var umember = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (umember == null)
            {
                return Unauthorized();
            }

            var dep = new Department() { DepartmentID = member.Department };
            _context.Attach(dep);
            var fac = new Faculty() { FacultyID = member.Faculty };
            _context.Attach(fac);

            var job = new Job() { JobID = member.Job };
            _context.Attach(job);
            string Img;
            if (member.Image != null)
            { 
                 Img = _imageService.SaveImg(member.Image); 
            }
            else
            {
                 Img = " ";
            }

            var Member = new Member()
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
                Job = job,
                Email = member.Email,
                Password = member.Password,
            };
            var entry = _context.Entry(Member);
            entry.State = EntityState.Modified;
            entry.Property(m => m.ImgUrl).IsModified = member.Image != null;
            

            await _context.SaveChangesAsync();
            return NoContent();
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
            var job = new Job() { JobID = member.Job };
            _context.Attach(job);
            string Img = _imageService.SaveImg(member.Image);

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
                Job = job,
                Email = member.Email,
                Password = member.Password,
  
            };

            _context.Member.Add(newMember);
            await _context.SaveChangesAsync();
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
