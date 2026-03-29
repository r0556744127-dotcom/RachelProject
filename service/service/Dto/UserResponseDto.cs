using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Dto
{
    public class UserResponseDto
    {
        // מזהה ייחודי של התלמיד במערכת (חשוב לשליפת מידע בהמשך)
        public int StudentId { get; set; }

        // שם מלא להצגה בראש המסך (למשל: "שלום, ישראל ישראלי")
        public string FullName { get; set; }

        // מספר זהות - לפעמים צריך אותו לתצוגה או אימות נוסף
        public string IdentityNumber { get; set; }

        // השדה הקריטי שהוספנו עכשיו:
        // האם ה-React צריך להכריח אותו להחליף סיסמה?
        public bool MustChangePassword { get; set; }

        // אם את משתמשת ב-Token (כמו JWT) לצורך אבטחה, הוא יחזור כאן
        public string Token { get; set; }
    }
}
