using _204362LibrarySystem.Models;
using _204362LibrarySystem.Models.DTO;
using LibrarySystem.Services;
using LibrarySystem.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Services
{
    public class MemberService
    {
        private readonly DatabaseContext _context;
        private readonly ImageService _imageService;

        public MemberService(DatabaseContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public Member Post(AddMemberDTO member)
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
        
            return newMember;
        }
        public Member Put(AddMemberDTO member)
        {
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

            var updateMember = new Member()
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
            return updateMember;
        }
       

    }
}
