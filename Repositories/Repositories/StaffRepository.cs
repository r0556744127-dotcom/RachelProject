using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class StaffRepository : IRepository<Staff>
    {
        private readonly IContext _context;

        public StaffRepository(IContext context)
        {
            _context = context;
        }

        public async Task<Staff> AddItem(Staff item)
        {
            await _context.Staffs.AddAsync(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var staff = await GetById(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                await _context.SaveAsync();
            }
        }

        public Task<List<Staff>> GetAllAsync()
        {
            return _context.Staffs.ToListAsync();
        }

        public async Task<Staff> GetById(int id)
        {
            return await _context.Staffs.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Staff> GetByEmail(string email)
        {
            return await _context.Staffs.FirstOrDefaultAsync(x => x.email == email);
        }
        public async Task<List<ClassRoom>> GetTeacherById(int id)
        {
            var staff = await GetById(id);
            return staff?.Classes ?? new List<ClassRoom>(); // מונע null
        }
        public async Task<User> GetByIdentityNumberAsync(string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Password == password);
        }
        public async Task UpdateItem(int id, Staff item)
        {
            var staff = await GetById(id);
            if (staff != null)
            {
                staff.FullName = item.FullName;
                staff.Lessons = item.Lessons;
                staff.Role = item.Role;
                staff.Classes = item.Classes;
                staff.email = item.email;
                await _context.SaveAsync();
            }
        }
    }
}