
using System;
using System.Collections.Generic;
using System.Text;
using User.OnBoarding.Application.CommunicationModel.Request;
using User.OnBoarding.ValueObject;

namespace User.OnBoarding.Application.ExtensionMethod
{
    public static class UserOnBoardingApplicationExt
    {
        public static UserSignUpValue ToUserValueSignUp(this UserSignUpRequest request)
        => new UserSignUpValue
            {
                Email = request.Email,
                Password = request.Password,
                UserDocument = request.UserDocument.Replace("-", string.Empty).Replace(".", string.Empty),
                UserName = request.UserName,
                UserId = Guid.NewGuid(),
                SignUpDate = DateTime.UtcNow
            };

        public static LoginValue ToValueLogin(this LoginRequest request)
        => new LoginValue
        {
            Email = request.Email,
            Password = request.Password
        };


    }
}
