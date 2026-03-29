using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class AssignmentRepository : IRepository<Assignment>
    {
        
        private readonly IContext _context;

        public AssignmentRepository(IContext context)
        {
            _context = context;
        }

        public async Task<Assignment> AddItem(Assignment item)
        {
            await _context.Assignments.AddAsync(item);
            await _context.SaveAsync();
            return item;

        }

      
        public async Task DeleteItem(int id)
        {
            var assignment = await GetById(id); // מחכים לקבל את האובייקט
            if (assignment != null)             // בדיקה שהפריט קיים
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveAsync();     // מחכים שהשמירה תסתיים
            }
        }
        public Task<List<Assignment>> GetAllAsync()
        {
            return _context.Assignments.ToListAsync();
        }


        public async Task<Assignment> GetById(int id)
        {
            var result = await _context.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        
        public async Task UpdateItem(int id, Assignment item)
        {
            var assignment =await GetById(id);
            if(assignment != null)
            {
                assignment.Title = item.Title;
                assignment.DueDate = item.DueDate;
                assignment.LessonId = item.LessonId;
                assignment.Lesson = item.Lesson;
                assignment.Submissions = item.Submissions;
                await _context.SaveAsync();
            }
            
        }

    }
}
