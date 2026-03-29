using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Repositories.Repositories
{
    public class LessonCategoryRepository : IRepository<LessonCategory>
    {
        private readonly IContext _context;
        public LessonCategoryRepository(IContext context)
        {
            _context = context;
        }

        public async Task<LessonCategory> AddItem(LessonCategory item)
        {
            await _context.LessonCategory.AddAsync(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var lessons = await GetById(id); // מחכים לקבל את האובייקט
            if (lessons != null)             // בדיקה שהפריט קיים
            {
                _context.LessonCategory.Remove(lessons);
                await _context.SaveAsync();     // מחכים שהשמירה תסתיים
            }
        }

        public Task<List<LessonCategory>> GetAllAsync()
        {
            return _context.LessonCategory.ToListAsync();
        }

        public async Task<LessonCategory> GetById(int id)
        {
            var result = await _context.LessonCategory.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public string NormalizeName(string name)
        {
            return Regex.Replace(name, @"[^א-ת0-9]", "").ToLower();
        }
        public async Task<List<LessonCategory>> GetLessonsByClassName(string className)
        {
            var normalized = NormalizeName(className);
            //מביא את כל הכיתות

            var classRoom = await _context.ClassRooms
                .ToListAsync();


            var matchClass = classRoom
                .FirstOrDefault(c => NormalizeName(c.Name) == normalized);

            if (matchClass == null)
                return new List<LessonCategory>();

            return await _context.LessonCategory
                .Where(l => l.ClassRoomId == matchClass.Id)
                .ToListAsync();
        }

        public async Task<LessonCategory> GetByName(string name)
        {
            var normalized = NormalizeName(name);

            var categories = await _context.LessonCategory.ToListAsync();

            return categories.FirstOrDefault(x => NormalizeName(x.Name) == normalized);
        }

        public async Task UpdateItem(int id, LessonCategory item)
        {
            var lessonCategory = await GetById(id);
            if (lessonCategory != null)
            {
                lessonCategory.Lessons = item.Lessons;
                lessonCategory.Name = item.Name;
                await _context.SaveAsync();
            }

        }

    }
}
