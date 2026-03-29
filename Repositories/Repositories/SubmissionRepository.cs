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
    public class SubmissionRepository : IRepository<Submission>
    {
        private readonly IContext _context;
        public SubmissionRepository(IContext context)
        {
            _context = context;
        }
        public async Task<Submission> AddItem(Submission item)
        {
            await _context.Submissions.AddAsync(item);
            await _context.SaveAsync();
            return item;
        }
        public async Task DeleteItem(int id)
        {

            var submissions = await GetById(id); // מחכים לקבל את האובייקט
            if (submissions != null)             // בדיקה שהפריט קיים
            {
                _context.Submissions.Remove(submissions);
                await _context.SaveAsync();     // מחכים שהשמירה תסתיים
            }
        }
        public Task<List<Submission>> GetAllAsync()
        {
            return _context.Submissions.ToListAsync();
        }
        public async Task<Submission> GetById(int id)
        {
            var result = await _context.Submissions.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<List<Submission>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Submissions.Where(s => s.StudentId == studentId).ToListAsync();
        }



        public async Task UpdateItem(int id,Submission item)
        {
            var submission=await GetById(id);
            if (submission != null)
            {
                submission.Student=item.Student;
                submission.StudentId=item.StudentId;
                submission.AssignmentId=item.AssignmentId;
                submission.Assignment=item.Assignment;
                submission.FilePath=item.FilePath;
                submission.Grade=item.Grade;
                submission.SubmittedAt=item.SubmittedAt;
                submission.TeacherComment=item.TeacherComment;
                await _context.SaveAsync();
            }
        }
    }
}
