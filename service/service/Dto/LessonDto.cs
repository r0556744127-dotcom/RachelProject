using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Dto
{
    public class LessonDto
    {
        public int idLesson { get; set; } 
        public int classId { get; set; }
        public string titelLesson { get; set; }
        public DateTime Date { get; set; }
        public int TeacherId { get; set; }
        public Uri RecordingLink { get; set; }
        // נתונים פשוטים לתצוגה
        //לתמלול על ידי ה AI
        public string? RecordingUrl { get; set; } // המרה של ה-Uri למחרוזת (string)
        public string? Summary { get; set; } // סיכום קצר
    }
}
