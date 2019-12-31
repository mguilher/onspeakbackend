using System.Collections.Generic;

namespace Lesson.Application.CommunicationModel.Response
{
    public class TeacherResponse : BaseResponse
    {
        public TeacherResponse():base()
        {
        }

        public IList<TeacherItemResponse> Items { get; set; }
    }
}
