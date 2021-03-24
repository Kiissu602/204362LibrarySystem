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
    public class JobsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public JobsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<List<JobFormDTO>> GetJob()
        {
            var JobRule = await _context.Job.Select(j => new JobFormDTO
            {
                JobID = j.JobID,
                JobName = j.JobName.ToString(),
                Amount =j.Rule.Amount,
                LimitDayBorrow = j.Rule.LimitDayBorrow,
                LimitDayBooking = j.Rule.LimitDayBooking,
                LostFines =j.Rule.LostFines,
                ReturnFines = j.Rule.ReturnFines,
                }).ToListAsync();

            return JobRule;
        }
            

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Job.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }
     
        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.JobID == id);
        }
    }
}
