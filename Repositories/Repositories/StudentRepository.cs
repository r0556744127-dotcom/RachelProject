using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly IContext _context;

        public StudentRepository(IContext context)
        {
            _context = context;
        }
        public async Task<Student> AddItem(Student item)
        {
            await _context.Student.AddAsync(item);
            await _context.SaveAsync();
            return item;
        }
        public async Task DeleteItem(int id)
        {
            var student = await GetById(id); // מחכים לקבל את האובייקט
            if (student != null)             // בדיקה שהפריט קיים
            {
                _context.Student.Remove(student);
                await _context.SaveAsync();     // מחכים שהשמירה תסתיים
            }
        }

        public Task<List<Student>> GetAllAsync()
        {
            return _context.Student.ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            var result = await _context.Student.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<User> GetByIdentityNumberAsync(string identityNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.IdentityNumber == identityNumber);
        }
        public async Task UpdateItem(int id, Student item)
        {
            var student = await GetById(id);
            if (student != null)
            {
                student.Submissions = item.Submissions;
                student.FullName = item.FullName;
                student.ClassRoom = item.ClassRoom;
                student.email = item.email;
                await _context.SaveAsync();
            }
        }

    }
}