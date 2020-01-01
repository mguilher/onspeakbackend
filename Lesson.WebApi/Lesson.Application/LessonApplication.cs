using Lesson.Application.CommunicationModel.Response;
using Lesson.Respository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lesson.Application
{
    public interface ILessonApplication
    {
        Task<LessonResponse> GetAllLessons();
        Task<LessonResponse> GetAllLessons(int teacherId);
        Task<TeacherResponse> GetAllTeachers();
        Task<LessonResponse> GetAllContent(string value);
    }

    public class LessonApplication : ILessonApplication
    {
        private readonly ILessonRepository _repository;
        private readonly ILogger<LessonApplication> _logger;

        public LessonApplication(ILessonRepository repository, ILogger<LessonApplication> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<TeacherResponse> GetAllTeachers()
        {
            var response = new TeacherResponse();
            try
            {
                var items = await _repository.GetAllTeachers();
                response = items.ToTeacherResponse();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "LessonApplication Error");
                response.ResponseType = ResponseTypeEnum.Error;
            }
            return response;
        }

        public async Task<LessonResponse> GetAllLessons()
        {
            var lesson = new LessonResponse();
            try
            {
                var items = await _repository.GetLessons();
                lesson = items.ToLessonResponse();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "LessonApplication Error");
                lesson.ResponseType = ResponseTypeEnum.Error;
            }
            return lesson;
        }

        public async Task<LessonResponse> GetAllContent(string value)
        {
            if (value == "*")
                return await GetAllLessons();

            var lesson = new LessonResponse();
            try
            {
                var items = await _repository.GetLessons(value);
                lesson = items.ToLessonResponse();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "LessonApplication Error");
                lesson.ResponseType = ResponseTypeEnum.Error;
            }
            return lesson;
        }

        public async Task<LessonResponse> GetAllLessons(int teacherId)
        {
            var lesson = new LessonResponse();
            try
            {
                var items = await _repository.GetLessons(teacherId);
                lesson = items.ToLessonResponse();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "LessonApplication Error");
                lesson.ResponseType = ResponseTypeEnum.Error;
            }
            return lesson;
        }
    }
}
