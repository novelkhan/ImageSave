using ImageSave.Data;
using ImageSave.IService;
using ImageSave.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageSave.Service
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Student GetSavedStudent()
        {
            //return _context.Students.SingleOrDefault();

            #pragma warning disable CS8603 // Possible null reference return.
            return _context.Students.OrderByDescending(x => x.StudentId).Select(x => new Student
            {
                StudentId = x.StudentId,
                Name = x.Name,
                Roll = x.Roll,
                Photo = x.Photo
            }).FirstOrDefault();
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public Student Save(Student oStudent)
        {
            _context.Students.Add(oStudent);
            _context.SaveChanges();
            return oStudent;
        }
    }
}
