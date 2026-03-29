using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class LessonRepository : IRepository<Lesson>
    {
        private readonly IContext _context;
        public LessonRepository(IContext context)
        {
            _context = context;
        }

        public async Task<Lesson> AddItem(Lesson item)
        {
            await _context.Lessons.AddAsync(item);
            await _context.SaveAsync();
            return item;
        }
        public async Task DeleteItem(int id)
        {
            var lessons = await GetById(id); // מחכים לקבל את האובייקט
            if (lessons != null)             // בדיקה שהפריט קיים
            {
                _context.Lessons.Remove(lessons);
                await _context.SaveAsync();     // מחכים שהשמירה תסתיים
            }
        }

        public Task<List<Lesson>> GetAllAsync()
        {
            return _context.Lessons.ToListAsync();
        }

        public async Task<Lesson> GetById(int id)
        {
            var result = await _context.Lessons.FirstOrDefaultAsync(x => x.idLesson == id);
            return result;
        }
        public async Task<List<Lesson>> GetLessonByIdLessonCategory(int idCategoy)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.LessonCategoryId == idCategoy);
            if (lesson == null)
            {
                return new List<Lesson>();
            }
            return await _context.Lessons.Where(l => l.LessonCategoryId == idCategoy).ToListAsync();
        }

        public async Task UpdateItem(int id, Lesson item)
        {
            var lesson = await GetById(id);
            if (lesson != null)
            {
                lesson.idLesson = item.idLesson;
                lesson.dateLesson = item.dateLesson;
                lesson.titelLesson = item.titelLesson;
                lesson.LessonCategory = item.LessonCategory;
                lesson.LessonCategoryId = item.LessonCategoryId;
                lesson.Assignments = item.Assignments;
                lesson.ClassRoom = item.ClassRoom;
                lesson.ClassRoomId = item.ClassRoomId;
                lesson.RecordingLink = item.RecordingLink;
                lesson.Teacher = item.Teacher;
                lesson.Assignments = item.Assignments.ToList();
                lesson.Transcript = item.Transcript;
                lesson.Summary = item.Summary;
                await _context.SaveAsync();
            }

        }
    }
}
