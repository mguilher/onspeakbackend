using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Application.CommunicationModel.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            ResponseType = ResponseTypeEnum.Info;
        }

        public ResponseTypeEnum ResponseType { get; set; }
        public string Message { get; set; }
        public bool Error => ResponseType != ResponseTypeEnum.Info;
    }

    public enum ResponseTypeEnum
    {
        Info, Error, ValidationProblem, NotFound, Conflict
    }
}
