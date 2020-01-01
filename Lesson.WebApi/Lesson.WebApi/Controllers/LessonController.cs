using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson.Application;
using Lesson.Application.CommunicationModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lesson.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        private readonly ILessonApplication _application;
        public LessonController(ILessonApplication application)
        {
            _application = application;
        }


        [HttpGet]
        public async Task<LessonResponse> GetLessons()
        {
            return await _application.GetAllLessons();
        }

        [HttpGet("{teacherId}")]
        public async Task<LessonResponse> GetLessons(int teacherId)
        {
            return await _application.GetAllLessons(teacherId);
        }

        [HttpGet("criteria/{value}")]
        public async Task<LessonResponse> GetLessons(string value)
        {
            return await _application.GetAllContent(value);
        }
    }
}
