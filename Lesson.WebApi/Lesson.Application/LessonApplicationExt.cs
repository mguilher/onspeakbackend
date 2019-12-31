using Lesson.Application.CommunicationModel.Response;
using Lesson.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lesson.Application
{
    public static class LessonApplicationExt
    {
        public static LessonResponse ToLessonResponse(this List<LessonValue> lessons)
        {
            var response = new LessonResponse();
            if (lessons.Count == 0)
                return response;

            var items = lessons.Select(l => new LessonItemResponse
            {
                LessonDescription = l.LessonDescription,
                LessonId = l.LessonId,
                LessonImageUrl = l.LessonImageUrl,
                LessonTitle = l.LessonTitle,
                LessonVideoUrl = l.LessonVideoUrl,
                TeacherCountry = l.TeacherCountry,
                TeacherImageUrl = l.TeacherImageUrl,
                TeacherName = l.TeacherName,
            }
            );

            response.Items = new List<LessonItemResponse>(items);
            return response;
        }

        public static TeacherResponse ToTeacherResponse(this List<TeacherValue> teachers)
        {
            var response = new TeacherResponse();
            if (teachers.Count == 0)
                return response;

            var items = teachers.Select(l => new TeacherItemResponse
            {
                TeacherId = l.TeacherId,
                TeacherName = l.TeacherName,
            }
            );

            response.Items = new List<TeacherItemResponse>(items);
            return response;
        }
    }
    
}
