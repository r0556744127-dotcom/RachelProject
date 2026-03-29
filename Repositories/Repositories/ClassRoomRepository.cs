using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ClassRoomRepository : IRepository<ClassRoom>
    {
        private readonly IContext _context;

        // שכחנו בנאי
        public ClassRoomRepository(IContext context)
        {
            _context = context;
        }

        public async Task<ClassRoom> AddItem(ClassRoom item)
        {
            await _context.ClassRooms.AddAsync(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var classRooms = await GetById(id);
            if (classRooms != null)
            {
                _context.ClassRooms.Remove(classRooms);
                await _context.SaveAsync();
            }
        }
        public async Task<List<ClassRoom>> GetAllAsync()
        {
            var list = await _context.ClassRooms.ToListAsync();
            return list;
        }
        public async Task<ClassRoom> GetById(int id)
        {
            var classRoom = await _context.ClassRooms
     .Include(c => c.Students)                       // טוען תלמידים
     .Include(c => c.LessonCategories)               // טוען קטגוריות
         .ThenInclude(lc => lc.Lessons)             // ← טוען גם את השיעורים בתוך הקטגוריה
     .FirstOrDefaultAsync(c => c.Id == id);
            return classRoom;
        }

        public async Task UpdateItem(int id, ClassRoom item)
        {
            var classRoom = await GetById(id);
            if (classRoom != null)
            {
                classRoom.Name = item.Name;
                classRoom.LessonCategories = item.LessonCategories;
                classRoom.Students = item.Students;

                await _context.SaveAsync();
            }
        }
    }
}