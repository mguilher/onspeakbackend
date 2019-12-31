using System;
using System.Text;

namespace Lesson.Application.CommunicationModel.Response
{
    public class LessonItemResponse
    {
        public int LessonId { get; set; }
        public string LessonImageUrl { get; set; }
        public string LessonTitle { get; set; }
        public string LessonVideoUrl { get; set; }

        public string LessonDescription { get; set; }
        public string TeacherName { get; set; }
        public string TeacherImageUrl { get; set; }
        public string TeacherCountry { get; set; }
    }
}
