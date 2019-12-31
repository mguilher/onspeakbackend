using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Application.AWSResponse
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public bool EverythingIsFine { get; set; }
    }
}
