using System.Collections.Generic;

namespace Lesson.Application.CommunicationModel.Response
{
    public class LessonResponse : BaseResponse
    {
        public LessonResponse():base()
        {
        }

        public IList<LessonItemResponse> Items { get; set; }
    }
}
