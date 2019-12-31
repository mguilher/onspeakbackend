using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Application.ExtensionMethod
{
    public static class OnBoardingApplicationExt
    {

        public static AWSResponse.UserResponse ToAWSUserResponse(this string json)
            => JsonConvert.DeserializeObject<AWSResponse.UserResponse>(json);

    }
}
