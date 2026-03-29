using AutoMapper;
using Repositories.models;
using service.Dto;

namespace service.services
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<Student, UserResponseDto>();

            // מיפוי מה-DTO של יצירת סטודנט חזרה לישות (עבור ה-Repository)
            CreateMap<CreateStudentDto, Student>();

            // אם שמות השדות שונים (למשל אם ב-Student זה 'Id' וב-DTO זה 'StudentId')
            // את צריכה להוסיף הגדרה ספציפית כך:
            CreateMap<Student, UserResponseDto>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Staff, UserResponseDto>();

            // מיפוי מה-DTO של יצירת סטודנט חזרה לישות (עבור ה-Repository)

            // אם שמות השדות שונים (למשל אם ב-Student זה 'Id' וב-DTO זה 'StudentId')
            // את צריכה להוסיף הגדרה ספציפית כך:
            CreateMap<Staff, UserResponseDto>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id));
            // מיפוי משתמש בסיסי
            CreateMap<User, UserDto>().ReverseMap();

            // יצירת מורה/מנהל - עכשיו כששני השדות נקראים FullName המיפוי אוטומטי
            CreateMap<CreateStaffDto, Staff>().ReverseMap();

            // יצירת תלמיד - גם כאן השדות זהים (FullName)
            CreateMap<Student, CreateStudentDto>()

                   .ForMember(dest => dest.studentId, opt => opt.MapFrom(src => src.Id))

                 .ForMember(dest => dest.ClassRoomId, opt => opt.MapFrom(src => src.ClassRoomId))

                .ReverseMap();

            // כיתות ושיעורים (הוספתי ReverseMap כדי למנוע שגיאות בשליפה)
            CreateMap<ClassRoom, ClassDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) // מיפוי ידני רק לשדה ששמו שונה
                .ReverseMap();
            // CreateMap<ClassRoom, ClassDetailDto>().ReverseMap();
            //CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Lesson, LessonDto>()
     .ForMember(dest => dest.idLesson, opt => opt.MapFrom(src => src.idLesson))
     .ForMember(dest => dest.titelLesson, opt => opt.MapFrom(src => src.titelLesson))
     .ForMember(dest => dest.RecordingLink, opt => opt.MapFrom(src => src.RecordingLink))
     .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary)).ReverseMap();

            // הגשות ומטלות
            CreateMap<CreateSubmissionDto, Submission>().ReverseMap();
            CreateMap<Submission, StudentSubmissionDto>().ReverseMap();
            CreateMap<Submission, TeacherSubmissionDto>().ReverseMap();
            CreateMap<UpdateGradeDto, Submission>().ReverseMap();
            CreateMap<Assignment, TeacherAssignmentDto>().ReverseMap();
            CreateMap<CreateAssignmentDto, Assignment>().ReverseMap();
            CreateMap<ClassDetailDto, ClassRoom>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClassName))
    .ReverseMap();
            // מיפוי נוסף לסטודנט אם יש צורך
            CreateMap<LessonCategory, CreateLessonDto>()
       .ForMember(dest => dest.lessonName, opt => opt.MapFrom(src => src.Name))
       .ForMember(dest => dest.classId, opt => opt.MapFrom(src => src.ClassRoomId));

            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<CreateLessonDto, LessonCategory>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.lessonName))
                .ForMember(dest => dest.ClassRoomId, opt => opt.MapFrom(src => src.classId))
                .ForMember(dest => dest.Lessons, opt => opt.Ignore()) // לא ממפים את הרשימה כרגע
                .ForMember(dest => dest.ClassRoom, opt => opt.Ignore()); // מניחים ש־ClassRoom נטען דרך Id        }
        }
    }
}