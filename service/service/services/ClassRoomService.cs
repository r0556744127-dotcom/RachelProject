using AutoMapper;
using Repositories.models;
using Repositories.Repositories;
using service.Dto;
using service.interfaces;


namespace service.Implementations 
{
    public class ClassRoomService : IClassRoomService
    {
        private readonly ClassRoomRepository _classRoomRepository;
        private readonly IMapper _mapper;

        public ClassRoomService(ClassRoomRepository classRoomRepository, IMapper mapper)
        {
            _classRoomRepository = classRoomRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateClassAsync(ClassDto classData)
        {
            var classRoom = _mapper.Map<ClassRoom>(classData);
            await _classRoomRepository.AddItem(classRoom);
            return true;
        }

        public async Task<ClassDetailDto> GetClassRoomDetailsAsync(int classId)
        {
            var classRoom = await _classRoomRepository.GetById(classId);
            if (classRoom == null) return null;

            // ממפה את שאר השדות
            var classDetail = _mapper.Map<ClassDetailDto>(classRoom);

            // ממיר את כל השיעורים מכל הקטגוריות לרשימה אחת ברמה של הכיתה
            classDetail.Lessons = classRoom.LessonCategories
                .SelectMany(lc => lc.Lessons)
                .Select(lesson => _mapper.Map<LessonDto>(lesson))
                .ToList();

            // ספירת תלמידים
            classDetail.StudentCount = classRoom.Students?.Count ?? 0;

            return classDetail;
        }

        public async Task<bool> DeleteClassRoomAsync(int classId)
        {
            var delClass = await _classRoomRepository.GetById(classId);
            if (delClass == null) return false;
            await _classRoomRepository.DeleteItem(classId);
            return true;
        }

        public async Task UpdateItem(int id, ClassDetailDto item)
        {
            await _classRoomRepository.UpdateItem(id, _mapper.Map<ClassRoom>(item));
        }

        public async Task<List<ClassDto>> getAllClassRoom()
        {
            var classRooms = await _classRoomRepository.GetAllAsync();
            return _mapper.Map<List<ClassDto>>(classRooms);
        }
    }
}