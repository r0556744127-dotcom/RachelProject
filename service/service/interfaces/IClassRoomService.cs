using service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.interfaces
{
    public interface IClassRoomService
    {
        // יצירת כיתה חדשה צריך לקבל שם ו ID
        Task<bool> CreateClassAsync(ClassDto classData);
        // שליפת פרטי כיתה קיימת כולל חישוב כמות תלמידים
        Task<ClassDetailDto> GetClassRoomDetailsAsync(int classId);
        Task<bool> DeleteClassRoomAsync(int classId);
        Task UpdateItem(int id, ClassDetailDto item);
        Task<List<ClassDto>> getAllClassRoom();
    }
}
