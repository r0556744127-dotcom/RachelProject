using Repositories.models;
using service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.interfaces
{
    public interface IStaffService
    {
        Task<UserResponseDto> GetStaffLoginAsync(LoginUser loginUser);
        Task<bool> UpdateInitialPasswordAsync(int staffId, string newPassword);
        Task<Staff?> CreateStaffMemberAsync(CreateStaffDto staffData);
        //מורה רוצה לראות את פרטי התלמיד
        Task<StudentDto> GetStudentProgressAsync(int studentId);
        //האם אני המורה של התלמיד
        Task<bool> IsTeacherAssignedToClass(int teacherId, int classId, int studentId);
        Task UpdateItem(int id, CreateStaffDto item);
        Task<Staff> GetStuffById(int id);
        Task<List<CreateStaffDto>> GetAllStaff();

    }
}
