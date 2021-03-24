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
    public class FacultiesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FacultiesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Faculties
        [HttpGet]
        public async Task<List<FacultyDTO>> GetFaculty()
        {
            var Facluty = await _context.Faculty.Select(f => new FacultyDTO { FacultyID = f.FacultyID, FacultyName =f.FacultyName }).ToListAsync();
            return Facluty;
        }

        // GET: api/Faculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFaculty(int id)
        {
            var faculty = await _context.Faculty.Include(f => f.Departmentlist).FirstOrDefaultAsync(f => f.FacultyID == id);

            if (faculty == null)
            {
                return NotFound();
            }

            return faculty;
        }

    }
}
